using GMAO.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Services.Authentication
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor contextAccessor;

        public AuthenticatedUser(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            var userIdClaim = contextAccessor.HttpContext?.User?.FindFirstValue("uid");
            var tenantIdClaim = contextAccessor.HttpContext?.User?.FindFirstValue("tenant_id");
            if (userIdClaim is not null && Guid.TryParse(userIdClaim, out var userId))
                UserId = userId;
            if (tenantIdClaim is not null && Guid.TryParse(tenantIdClaim, out var tenantId))
                TenantId = tenantId;
        }
        public Guid UserId { get; private set; }

        public Guid TenantId { get; private set; }

        public bool IsAccountConfirmed()
        {
            var emailConfirClaim = contextAccessor.HttpContext?.User?.FindFirstValue("email_confirmed");
            if (emailConfirClaim is not null && bool.TryParse(emailConfirClaim, out var emailConfirmed))
                return emailConfirmed;
            return false;
        }

        public bool IsAuthenticated()
        {
            return !UserId.Equals(Guid.Empty) && !TenantId.Equals(Guid.Empty);
        }


    }
}
