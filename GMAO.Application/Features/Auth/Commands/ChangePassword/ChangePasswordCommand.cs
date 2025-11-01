using GMAO.Application.Common.Authorization;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ResponseResult<bool>>, IRequiredPermission
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; } = string.Empty;

        public string[] RequiredPermissions => [];

        public string[] RequiredRoles => [];

        public bool MustMatchTenant => true;

        public bool RequireAuthentication => true;
        public bool CheckEmailConfirmedAndPasswordChanged => false;
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResponseResult<bool>>
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticatedUser _authenticatedUser;

        public ChangePasswordCommandHandler(IAccountService accountService, IAuthenticatedUser authenticatedUser)
        {
            this._accountService = accountService;
            this._authenticatedUser = authenticatedUser;
        }
        public async Task<ResponseResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.ChangePasswordAsync(_authenticatedUser.UserId, request.NewPassword, request.OldPassword);
            return ResponseResult<bool>.OkResult(result);
        }
    }
}
