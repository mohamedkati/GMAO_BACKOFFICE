using GMAO.Application.Features.Auth.Queries.Login.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(LoggedUserDtoForJwt loggedUser);
    }
}
