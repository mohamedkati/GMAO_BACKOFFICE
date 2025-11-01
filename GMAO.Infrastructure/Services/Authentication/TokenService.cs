using GMAO.Application.Common.AppSettings;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.Auth.Queries.Login.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

           
        public async Task<string> GenerateTokenAsync(LoggedUserDtoForJwt loggedUser)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < loggedUser.AppRoles?.Count; i++)
            {
                roleClaims.Add(new Claim("AppRoles", loggedUser.AppRoles[i]));
            }

            for (int i = 0; i < loggedUser.DomainRoles?.Count; i++)
            {
                roleClaims.Add(new Claim("DomainRoles", loggedUser.DomainRoles[i]));
            }

            for (int i = 0; i < loggedUser.DomainPermissions?.Count; i++)
            {
                roleClaims.Add(new Claim("DomainPermissions", loggedUser.DomainPermissions[i]));
            }


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, loggedUser.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, loggedUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, loggedUser.Email),
                new Claim("uid", loggedUser.Id.ToString()),
                new Claim("tenant_id",loggedUser.TenantId.ToString()),
                new Claim("email_confirmed", loggedUser.EmailConfirmed.ToString()),
                new Claim("phone_number_confirmed", loggedUser.PhoneNumberConfirmed.ToString()),
                new Claim("default_password_changed", loggedUser.DefaultPasswordChanged.ToString()),
                //new Claim("ip", ipAddress)
            }
            //.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            await Task.CompletedTask;

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
