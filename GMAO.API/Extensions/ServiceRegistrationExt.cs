using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GMAO.API.Extensions
{
    public static class ServiceRegistrationExt
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(opt =>
               {
                   opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
                .AddJwtBearer(cnf =>
                {
                    cnf.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],

                    };
                });

            return services;
        }
    }
}
