
using GMAO.Application.Features.Staffs.Commands.CreateUser;
using GMAO.Application.Features.Staffs.Queries.GetStaffsAsKeyValue;

namespace GMAO.API.Controllers.v1
{
    public class StaffsController : AuthorizedController
    {
        [HttpGet("select-as-key-value")]
        public async Task<IActionResult> GetCommercialsAsKeyValue([FromQuery] string role, [FromQuery] string search)
        {
            var query = new GetStaffsAsKeyValueQuery
            {
                Role = role,
                Search = search
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

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
