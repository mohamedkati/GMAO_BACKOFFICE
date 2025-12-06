using GMAO.Application.Features.Auth.Queries.Login.DTOs;

namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(LoggedUserDtoForJwt loggedUser);
    }
}
