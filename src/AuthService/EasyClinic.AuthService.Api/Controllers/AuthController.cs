using EasyClinic.AuthService.Application.Commands;
using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request, 
            CancellationToken cancellationToken = default)
        {
            var userData = await _mediator.Send(request, cancellationToken);

            return Ok(userData);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request, 
            CancellationToken cancellationToken = default)
        {
            var userData = await _mediator.Send(request, cancellationToken);

            return Ok(userData);
        }

        [HttpGet("email-exists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CheckEmailExistence(
            [FromQuery] CheckEmailExistenceQuery request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("fetch-current-user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserToReturnDto>> GetCurrentUser(
            CancellationToken cancellationToken = default)
        {
            var request = new GetCurrentUserQuery{User = User};
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("send-account-confirm")]
        public async Task<IActionResult> SendAccountConfirmationEmail(
            CancellationToken cancellationToken = default)
        {
            var request = new ResendAccountConfirmCommand{Username = User.Identity?.Name!};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string userId, [FromQuery] string token,
            CancellationToken cancellationToken = default)
        {
            var request = new VerifyEmailCommand{UserId = userId, Token = token};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
        
        [Authorize]
        [HttpPost("send-password-reset")]
        public async Task<IActionResult> SendPasswordResetEmail(CancellationToken cancellationToken = default)
        {
            var request = new SendPasswordResetCommand{Username = User.Identity?.Name!};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword([FromQuery] string userId, [FromQuery] string token,
            CancellationToken cancellationToken = default)
        {
            var request = new ChangePasswordQuery{Token = token, UserId = userId};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordSubmit([FromQuery] string userId, [FromQuery] string token, 
            [FromHeader] string newPassword, CancellationToken cancellationToken = default)
        {
            var request = new ChangePasswordSubmitCommand
            {
                Token = token, 
                UserId = userId, 
                NewPass = newPassword
            };
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
    }
}