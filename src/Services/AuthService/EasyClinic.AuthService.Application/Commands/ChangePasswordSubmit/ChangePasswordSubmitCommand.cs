using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace EasyClinic.AuthService.Application.Commands.ChangePasswordSubmit
{
    /// <summary>
    /// Command for changing user's password by submitting a form
    /// </summary>
    public record ChangePasswordSubmitCommand : IRequest
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    };

    /// <summary>
    /// Handler for <see cref="ChangePasswordSubmitCommand"/>
    /// </summary>
    public class ChangePasswordSubmitCommandHandler : IRequestHandler<ChangePasswordSubmitCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordSubmitCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Changes user's password if the token is valid for user.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <exception cref="NotFoundException">
        /// Thrown when user does not exist.
        /// </exception>
        /// <exception cref="BadRequestException">
        /// Thrown when the token is invalid for user.
        /// </exception>
        /// <returns></returns>
        public async Task Handle(ChangePasswordSubmitCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User with id {request.UserId} does not exist");
            }

            var token = Uri.UnescapeDataString(request.Token);

            var tokenCheck = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
