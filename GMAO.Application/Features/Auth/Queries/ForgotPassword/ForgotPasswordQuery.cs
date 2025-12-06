using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using MediatR;

namespace GMAO.Application.Features.Auth.Queries.ForgotPassword
{
    public class ForgotPasswordQuery : IRequest<ResponseResult<string>>
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQuery, ResponseResult<string>>
    {
        private readonly IAccountService _accountService;

        public ForgotPasswordQueryHandler(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        public async Task<ResponseResult<string>> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            await _accountService.RequestForgotPasswordLinkAsync(request.Email);
            return ResponseResult<string>.OkResult("Le lien pour réinitialise le mot de passe a été envoyé à votre email.");
        }
    }
}
