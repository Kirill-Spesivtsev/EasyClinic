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
    [Route("api/v1/auth")]
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
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] CheckEmailExistenceQuery request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("fetch-user")]
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
    }
}