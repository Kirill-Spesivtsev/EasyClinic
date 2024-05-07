using EasyClinic.AuthService.Application.Commands;
using EasyClinic.AuthService.Application.Commands.ChangePasswordSubmit;
using EasyClinic.AuthService.Application.Commands.LoginUser;
using EasyClinic.AuthService.Application.Commands.RegisterUser;
using EasyClinic.AuthService.Application.Commands.ResendAccountConfirmation;
using EasyClinic.AuthService.Application.Commands.SendPasswordReset;
using EasyClinic.AuthService.Application.Commands.VerifyEmail;
using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Queries;
using EasyClinic.AuthService.Application.Queries.ChangePassword;
using EasyClinic.AuthService.Application.Queries.CheckEmailExistence;
using EasyClinic.AuthService.Application.Queries.GetAllUserRolesById;
using EasyClinic.AuthService.Application.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using System.Threading;

namespace EasyClinic.AuthService.Api.Controllers
{
    /// <summary>
    /// Controller for user authentication and authorization
    /// </summary>
    [ApiController]
    [Route("api/v1/account")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Returns JWT token if authentication was successful.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Registers new user, sends confirmation email.
        /// Returns JWT token if authentication was successful.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets current authenticated user.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get-current-user")]
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

        /// <summary>
        /// Verifies email address if the provided token is valid for user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("verify-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyEmail([FromQuery] string userId, [FromQuery] string token,
            CancellationToken cancellationToken = default)
        {
            var request = new VerifyEmailCommand{UserId = userId, Token = token};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Resends email (account) confirmation link to email.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("resend-account-confirmation-link")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendAccountConfirmationEmail(
            CancellationToken cancellationToken = default)
        {
            var request = new ResendAccountConfirmCommand{Username = User.Identity?.Name!};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
        
        /// <summary>
        /// Sends password reset link to email.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("send-password-reset-link")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendPasswordResetEmail(CancellationToken cancellationToken = default)
        {
            var request = new SendPasswordResetCommand{Username = User.Identity?.Name!};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }


        /// <summary>
        /// Gets password change form.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword([FromQuery] string userId, [FromQuery] string token,
            CancellationToken cancellationToken = default)
        {
            var request = new ChangePasswordQuery{Token = token, UserId = userId};
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Changes user's password if the token is valid for user.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePasswordSubmit(ChangePasswordSubmitCommand request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);

            return Ok();
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
		
		[HttpGet("get-current-user-roles")]
        [Authorize(Roles = "Receptionist, Admin")]
		public async Task<IActionResult> GetCurrentUserRoles(
            CancellationToken cancellationToken = default)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var request = new GetAllUserRolesByIdQuery{UserId = userId};

            var roles = await _mediator.Send(request, cancellationToken);


			return Ok(roles);
		}

        [HttpGet("get-user-roles")]
        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserRoles(string userId,
            CancellationToken cancellationToken = default)
		{
			var id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var request = new GetAllUserRolesByIdQuery{UserId = id};

            var roles = await _mediator.Send(request, cancellationToken);


			return Ok(roles);
		}
    }

}