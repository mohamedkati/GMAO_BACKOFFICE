using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.me.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.me.Queries.MyInfo
{
    public class MeQuery : IRequest<ResponseResult<MeQueryDto>>
    {
    }

    public class MeQueryHandler : IRequestHandler<MeQuery, ResponseResult<MeQueryDto>>
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IAccountService _accountService;

        public MeQueryHandler(IAuthenticatedUser authenticatedUser, IAccountService accountService)
        {
            this._authenticatedUser = authenticatedUser;
            this._accountService = accountService;
        }
        public async Task<ResponseResult<MeQueryDto>> Handle(MeQuery request, CancellationToken cancellationToken)
        {
            var user = await _accountService.GetUserInfoAsync(_authenticatedUser.UserId, _authenticatedUser.TenantId);
            return ResponseResult<MeQueryDto>.OkResult(user);
        }
    }
}
