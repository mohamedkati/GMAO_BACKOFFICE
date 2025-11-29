
using GMAO.Application.Features.Auth.Commands.ChangePassword;
using GMAO.Application.Features.Auth.Commands.ConfirmEmail;
using GMAO.Application.Features.Auth.Commands.ForgotPassword;
using GMAO.Application.Features.Auth.Queries.EmailConfirmation;
using GMAO.Application.Features.Auth.Queries.ForgotPassword;
using GMAO.Application.Features.Auth.Queries.Login;
using GMAO.Application.Features.me.Queries.MyInfo;
using Microsoft.AspNetCore.Authorization;

namespace GMAO.API.Controllers.v1
{
    public class AuthController : BaseControllerApi
    {

        public AuthController()
        {
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AcceptVerbs("POST")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            return Ok(await Mediator.Send(loginQuery));
        }

        [HttpGet("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestForgotPassword([FromQuery] ForgotPasswordQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromQuery] ForgotPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("request-email-confirmation")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestEmailConfirmation([FromQuery] RequestEmailConfirmationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("confirm-email")]
        [HttpGet("confirm-account")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpGet("verify-request")]
        //[HttpGet("account/me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var query = new MeQuery();
            return Ok(await Mediator.Send(query));
        }
    }
}
