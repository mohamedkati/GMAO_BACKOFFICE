using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Queries.EmailConfirmation
{
    public class RequestEmailConfirmationQuery : IRequest<ResponseResult<string>>
    {
        public string Email { get; set; }
    }

    public class RequestEmailConfirmationQueryHandler : IRequestHandler<RequestEmailConfirmationQuery, ResponseResult<string>>
    {
        private readonly IAccountService _accountService;

        public RequestEmailConfirmationQueryHandler(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        public async Task<ResponseResult<string>> Handle(RequestEmailConfirmationQuery request, CancellationToken cancellationToken)
        {
            await _accountService.RequestEmailConfirmAsync(request.Email);
            return ResponseResult<string>.OkResult("L'email de confirmation de votre compte a été envoyé avec succès");
        }
    }
}
