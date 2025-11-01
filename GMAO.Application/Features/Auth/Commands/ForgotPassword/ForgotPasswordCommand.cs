using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<ResponseResult<bool>>
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }

    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ResponseResult<bool>>
    {
        private readonly IAccountService _accountService;

        public ForgotPasswordCommandHandler(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public async Task<ResponseResult<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.ForgotPasswordAsync(request.UserId, request.Code, request.Password);
            return ResponseResult<bool>.OkResult(result);
        }
    }
}
