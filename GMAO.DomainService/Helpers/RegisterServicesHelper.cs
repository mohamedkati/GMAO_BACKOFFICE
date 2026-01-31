using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.DomainService.Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.DomainService.Helpers
{
    public static class RegisterServicesHelper
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ISiteServiceAsync, SiteServiceAsync>();
        }
    }
}
