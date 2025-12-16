using GMAO.Application.Common.Authorization;
using GMAO.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GMAO.Application.Common.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not IRequiredPermission authorization)
                return await next(cancellationToken);

            if (!authorization.RequireAuthentication)
                return await next(cancellationToken);

            var user = _contextAccessor.HttpContext.User;
            if (user is null || user.Identity is null || !user.Identity.IsAuthenticated)
                throw new UnAuthenticatedException("User not authenticated.");

            if (authorization.CheckEmailConfirmedAndPasswordChanged == true)
            {
                var emailConfirmedClaim = user.FindFirst("email_confirmed")?.Value;
                var passwordChangedClaim = user.FindFirst("default_password_changed")?.Value;

                // TODO Look AFTER
                //if (emailConfirmedClaim?.ToLower() != "true")
                //    throw new UnAuthorizedException("User email is not confirmed.");
                //if (passwordChangedClaim?.ToLower() != "true")
                //    throw new UnAuthorizedException("User must change password.");
            }

            if (authorization.RequiredRoles.Any())
            {
                var userRoles = user.Claims
                    .Where(c => c.Type == "DomainRoles")
                    .Select(c => c.Value)
                    .ToList();

                if (!authorization.RequiredRoles.Any(x => userRoles.Contains(x)))
                    throw new UnAuthorizedException("Vous n'avez pas la permission pour cette action");
            }

            if (authorization.RequiredPermissions.Any())
            {
                var permissions = user.Claims
                    .Where(c => c.Type == "DomainPermissions")
                    .Select(c => c.Value)
                    .ToList();

                if (!authorization.RequiredPermissions.Any(x => permissions.Contains(x)))
                    throw new UnAuthorizedException("Vous n'avez pas la permission pour cette action");
            }

            if (authorization.MustMatchTenant)
            {
                var userTenantId = user.FindFirst("tenant_id")?.Value;
                var tenantIdFromHeader = _contextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault();
                 //TODO LOOK AFTER
                //if (tenantIdFromHeader != null && userTenantId != tenantIdFromHeader)
                //    throw new UnAuthorizedException("Forbidden: Tenant mismatch.");
            }

            return await next(cancellationToken);
        }
    }
}
