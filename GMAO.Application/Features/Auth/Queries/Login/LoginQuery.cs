using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.Auth.Queries.Login.DTOs;
using GMAO.Application.Helpers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<ResponseResult<LoggedUserDto>>
    {

    }

    public class LoginQueryHandler : IRequestHandler<LoginQuery, ResponseResult<LoggedUserDto>>
    {
        private readonly IAccountService _accountService;

        public LoginQueryHandler(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        public async Task<ResponseResult<LoggedUserDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = await _accountService.LoginAsync(request.Email, request.Password);
            return ResponseResult<LoggedUserDto>.OkResult(result);
        }
    }
}
