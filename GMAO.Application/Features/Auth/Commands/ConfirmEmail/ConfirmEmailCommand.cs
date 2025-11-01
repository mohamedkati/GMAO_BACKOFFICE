using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<ResponseResult<bool>>
    {
        public string Code { get; set; }
        public string UserId { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ResponseResult<bool>>
    {
        private readonly IAccountService _accountService;

        public ConfirmEmailCommandHandler(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public async Task<ResponseResult<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.ConfirmEmailAsync(request.UserId, request.Code);
            return ResponseResult<bool>.OkResult(result);
        }
    }
}
