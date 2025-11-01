using GMAO.Application.Common.AppSettings;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Common.Interfaces.Services;
using GMAO.Application.Features.Auth.Queries.Login.DTOs;
using GMAO.Domain.Entities.Auth;
using GMAO.Infrastructure.DIHelpers;
using GMAO.Infrastructure.Persistance.Identity;
using GMAO.Shared.Templates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Services.Authentication
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenService tokenService;
        private readonly IAppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly AppSetting _appSetting;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ITokenService tokenService, IAppDbContext context, IOptions<AppSetting> appSetting, IEmailService emailService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.tokenService = tokenService;
            this._context = context;
            this._emailService = emailService;
            this._appSetting = appSetting.Value;
        }
        public Task<bool> ConfirmPhoneNumberAsync(string userId, string token, string checkNumber)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Authenticate user and generate JWT token, if the credentials are valid, otherwise throw ValidationException, also check if the user is assigned to a tenant, /// if not, throw ValidationException, then generate the token with user and tenant info, return the token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AppValidationException"></exception>
        public async Task<LoggedUserDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new AppValidationException(new Dictionary<string, string[]>() { { "credentials", new[] { "Invalid credentials !" } } });
            }
            var tenant = await _context.SetEntity<TenantUser>().FirstOrDefaultAsync(tu => tu.UserId == user.Id);
            if (tenant is null)
            {
                throw new AppValidationException(new Dictionary<string, string[]>() { { "tenant", new[] { "No tenant assigned to this user !" } } });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var domainPermissions = await _context.SetEntity<TenantUser>()
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .Where(t => t.Id == tenant.Id)
                .SelectMany(x => x.Role.Permissions)
                .Select(x => x.Code)
                .ToListAsync();

            var domainRoles = await _context.SetEntity<TenantUser>().Include(x => x.Role)
                .Where(t => t.Id == tenant.Id)
                .Select(x => x.Role.Name)
                .ToListAsync();

            var loggedUser = new LoggedUserDtoForJwt()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                TenantId = tenant.TenantId,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                DefaultPasswordChanged = user.DefaultPasswordChanged,
                AppRoles = roles.ToList(),
                DomainRoles = domainRoles.ToList(),
                DomainPermissions = domainPermissions.ToList()
            };

            var token = await tokenService.GenerateTokenAsync(loggedUser);
            return new LoggedUserDto()
            {
                Id = user.Id,
                AccountConfirmed = user.EmailConfirmed,
                TenantId = tenant.Id,
                FullName = user.UserName,
                Token = token,
                UserName = user.UserName
            };
        }

        /// <summary>
        /// Register a new user with username, phone number, email and password, validate if the username and email are unique, otherwise throw ValidationException, create the user and assign default role, return the userId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AppValidationException"></exception>
        public async Task<Guid> RegisterAsync(string username, string phoneNumber, string email, string password)
        {
            await ValidateUserRegistration(username, email);

            var newUser = new ApplicationUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phoneNumber,
                //EmailConfirmed = true // in real case, send confirmation email
            };
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });
                throw new AppValidationException(errors);
            }
            // Assign default role to user (e.g., "User")
            //var defaultRoleName = "User";
            //var roleExists = await _roleManager.RoleExistsAsync(defaultRoleName);
            //if (!roleExists)
            //{
            //    var role = new ApplicationRole();
            //    await _roleManager.CreateAsync(role);
            //}
            //await _userManager.AddToRoleAsync(newUser, defaultRoleName);
            return newUser.Id;
        }

        /// <summary>
        /// Request a password reset link to be sent to the user's email address : generates a token and send the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestForgotPasswordLinkAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = GetUrl("/auth/reset-password", _appSetting.ClientAppUrl, token, user.Id);
            var template = AuthEmailTemplateHelper.GetResetPasswordEmailContent(user.UserName, url);

            await _emailService.SendEmailAsync(email, "Reset your password", template);
        }

        /// <summary>
        /// Reset password when the user is not logged in, important note here : the user must have a valid token sent to his email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="AppValidationException"></exception>
        public async Task<bool> ForgotPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userId);
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });
                throw new AppValidationException(errors);
            }
            user.DefaultPasswordChanged = true;
            await _userManager.UpdateAsync(user);
            return true;
        }
        public Task<bool> ForceChangePasswordForFirstTimeAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reset password when the user is logged in important note here : the user must be logged in and the userId must be extracted from the token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="AppValidationException"></exception>
        public async Task<bool> ChangePasswordAsync(Guid userId, string newPassword, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userId);
            }
            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!isOldPasswordValid)
            {
                throw new AppValidationException(new Dictionary<string, string[]>() { { "oldPassword", new[] { "Old password is incorrect !" } } });
            }

            var resukt = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!resukt.Succeeded)
            {
                var errors = resukt.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });
                throw new AppValidationException(errors);
            }

            user.DefaultPasswordChanged = true;
            await _userManager.UpdateAsync(user);
            return true;
        }

        /// <summary>
        /// Confirm email after requesting a confirm-email via a link to the target email address : the user clicks on the link sent to his email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            return await ConfirmUserRegistrationAsync(token, userId);
        }

        /// <summary>
        /// Confirm user registration after signing up : the user clicks on the link sent to his email, this is similar to confirm email, but we keep it for clarity, code is the token here
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="AppValidationException"></exception>
        public async Task<bool> ConfirmUserRegistrationAsync(string code, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userId);
            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });
                throw new AppValidationException(errors);
            }
            return result.Succeeded;
        }


        /// <summary>
        /// Send confirmation email after user registration with a link to confirm the account, this is called after successful registration, important note : the user is not yet confirmed here and the token is generated here and sent via email with his current auto generated password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task SendConfirmEmailAfterRegistrationAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), email);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = GetUrl("/auth/confirm-account", _appSetting.ClientAppUrl, token, user.Id);
            var template = AuthEmailTemplateHelper.GetAccountCreatedEmailContent(user.UserName, password, url);

            await _emailService.SendEmailAsync(email, "Account Created Successfully", template);
        }

        /// <summary>
        /// Request email confirmation link to be sent to the user's email address : generates a token and send the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestEmailConfirmAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return;

            if (user.EmailConfirmed)
                return;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = GetUrl("/auth/confirm-email", _appSetting.ClientAppUrl, token, user.Id);
            var template = AuthEmailTemplateHelper.GetConfirmationEmailContent(user.UserName, url);

            await _emailService.SendEmailAsync(email, "Confirm your email", template);
        }


        /// <summary>
        /// Validate if the username and email are unique during registration
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="AppValidationException"></exception>
        private async Task ValidateUserRegistration(string username, string email)
        {
            var userExist = await _userManager.FindByEmailAsync(username);
            if (userExist is not null) throw new AppValidationException(new Dictionary<string, string[]>() { { "email", new[] { "Email already used !" } } });

            userExist = await _userManager.FindByNameAsync(username);
            if (userExist is not null) throw new AppValidationException(new Dictionary<string, string[]>() { { "username", new[] { "Username already used !" } } });
        }

        /// <summary>
        /// Generate URL with token and userId as query parameters
        /// </summary>
        /// <param name="route"></param>
        /// <param name="origin"></param>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GetUrl(string route, string origin, string token, Guid? userId = null)
        {
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var _enpointUri = new Uri(string.Concat($"{origin}", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "code", code);
            if (userId.HasValue)
                verificationUri = QueryHelpers.AddQueryString(verificationUri, "userId", userId.ToString());
            return verificationUri;
        }


    }
}
