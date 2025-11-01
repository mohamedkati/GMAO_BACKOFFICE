using GMAO.Application.Common.Authorization;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffEntity = GMAO.Domain.Entities.Staff;
namespace GMAO.Application.Features.Staff.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string FirstName, string LastName, string PhoneNumber, Guid RoleId) : IRequest<ResponseResult<Guid>>, IRequiredPermission
    {
        public string[] RequiredPermissions => [];
        public string[] RequiredRoles => ["Admin"];
        public bool MustMatchTenant => true;
        public bool RequireAuthentication => true;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseResult<Guid>>
    {
        private readonly IAccountService _accountService;
        private readonly IAppDbContext _context;
        private readonly IAuthenticatedUser _authenticatedUser;

        public CreateUserCommandHandler(IAccountService accountService, IAppDbContext context, IAuthenticatedUser authenticatedUser)
        {
            this._accountService = accountService;
            this._context = context;
            this._authenticatedUser = authenticatedUser;
        }

        public async Task<ResponseResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var password = GenerateNewRandomPassword(12);
                await _context.StartTransactionAsync();
                var userId = await _accountService.RegisterAsync(request.UserName, request.PhoneNumber, request.Email, password);

                // Assign Role to User TODO.

                var staff = new StaffEntity(userId, _authenticatedUser.TenantId, request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.UserName, request.RoleId, password);
                var tenantUser = new TenantUser(_authenticatedUser.TenantId, userId, request.RoleId, userId);
                await _context.SetEntity<StaffEntity>().AddAsync(staff);
                await _context.SetEntity<TenantUser>().AddAsync(tenantUser);
                await _context.SaveAllAsync(cancellationToken);
                await _context.CommitTransactionAsync();
                return ResponseResult<Guid>.OkResult(userId);
            }
            catch (AppValidationException ex)
            {
                await _context.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsync();
                throw new ApiException("An error occurred while creating the user.", ex);
            }

        }

        private string GenerateNewRandomPassword(int length)
        {
            string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            string Numbers = "0123456789";
            string SpecialCharacters = "!@#$%^&*()-_=+[]{}|;:,.<>?/";
            Random random = new Random();

            // Ensure the password contains at least one of each required character type
            char[] password = new char[length];
            password[0] = UppercaseLetters[random.Next(UppercaseLetters.Length)];
            password[1] = LowercaseLetters[random.Next(LowercaseLetters.Length)];
            password[2] = Numbers[random.Next(Numbers.Length)];
            password[3] = SpecialCharacters[random.Next(SpecialCharacters.Length)];

            // Fill the remaining characters with a random mix of all character sets
            string allCharacters = UppercaseLetters + LowercaseLetters + Numbers + SpecialCharacters;
            for (int i = 4; i < length; i++)
            {
                password[i] = allCharacters[random.Next(allCharacters.Length)];
            }

            // Shuffle the password to randomize the order
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}
