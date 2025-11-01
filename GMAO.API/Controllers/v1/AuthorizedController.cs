using Microsoft.AspNetCore.Authorization;

namespace GMAO.API.Controllers.v1
{
    [Authorize]
    public abstract class AuthorizedController : BaseControllerApi
    {
    }
}
