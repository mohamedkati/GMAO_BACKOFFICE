using GMAO.Application.Features.Staff.Commands.CreateUser;

namespace GMAO.API.Controllers.v1
{
    public class StaffController : AuthorizedController
    {

        [HttpPost("create-staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<IActionResult> CreateStaff([FromBody] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
