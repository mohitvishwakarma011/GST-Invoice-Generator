using GI.Application.DataTransferObjects.Auth;
using GI.Application.Features.Auth.Commands.HardResetPassword;
using GI.Application.Features.Auth.Commands.Login;
using GI.Application.Features.Auth.Commands.Logout;
using GI.Application.Features.Auth.Commands.Refresh;
using GI.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace GI.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;
        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            var response = new AuthResponseBase
            {
                AccessToken = result.AccessToken,
                BusinessName = result.BusinessName,
                Email = result.Email
            };
            Response.Cookies.Append(AppConstants.RefershTokenCookieKey, result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = result.RefreshTokenExpiry
            });
            return Ok(response);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken()
        {
            var cookie = Request.Cookies[AppConstants.RefershTokenCookieKey] ??
                            throw new UnauthorizedAccessException("Session has been expired. Please login!");
            var command = new RefreshAccessTokenCommand { RefreshToken = cookie };
            var result = await _mediator.Send(command);
            var response = new AuthResponseBase
            {
                AccessToken = result.AccessToken,
                BusinessName = result.BusinessName,
                Email = result.Email
            };
            Response.Cookies.Append(AppConstants.RefershTokenCookieKey, result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = result.RefreshTokenExpiry
            });
            return Ok(response);
        }

        [HttpPut("hard-reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HardResetPassword([FromBody] HardResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> LogoutUser([FromBody] LogoutCommand command)
        {
            Response.Cookies.Delete(AppConstants.RefershTokenCookieKey);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
