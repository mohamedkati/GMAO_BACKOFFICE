using GMAO.Infrastructure.Persistance.Identity;
using GMAO.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Services;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Infrastructure.Services;
using GMAO.Infrastructure.Services.Authentication;
using GMAO.Application.Common.Interfaces.Infrastructure;
using GMAO.Infrastructure.Infras;
using GMAO.Infrastructure.Persistance.UnitOfWork;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Infrastructure.Persistance.Repositories;

namespace GMAO.Infrastructure.DIHelpers
{
    public static class InfrastructureRegistration
    {
        public static IdentityBuilder AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //  Ajouter Identity basé sur ApplicationUser & ApplicationRole
            return services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedAccount = true;
                opt.ClaimsIdentity.UserIdClaimType = ClaimTypes.PrimarySid;
                opt.ClaimsIdentity.UserNameClaimType = ClaimTypes.NameIdentifier;
                opt.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        }

        public static void ConfigureInfraServices(this IServiceCollection services, IConfiguration config)
        {
            ConfigureRepositories(services);
            services.AddScoped<IDatetimeService, DatetimeService>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            services.AddScoped<ITokenService, TokenService>();
        }
        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IPropertyGroupRepository), typeof(PropertyGroupRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

        }
        public static void ConfigureAppAuthenticationServices(this IServiceCollection services)
        {
            //services.AddScoped<IAppDbContext>(scp => scp.GetRequiredService<AppDbContext>());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
