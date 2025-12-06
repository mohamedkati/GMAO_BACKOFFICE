using FluentValidation;
using GMAO.Application.Common.Behaviours;
using GMAO.Application.Features.Auth.Queries.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GMAO.Application.DIExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(LoginQuery).Assembly);
            // Enregistrement des Behaviors (Pipeline MediatR)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(cnf => cnf.AddMaps(Assembly.GetExecutingAssembly()));

            return services;
        }

    }
}
