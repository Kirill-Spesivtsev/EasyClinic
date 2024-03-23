using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyClinic.AuthService.Application.Commands.ResendAccountConfirmation
{
    /// <summary>
    /// Resends account confirmation message to user's email
    /// </summary>
    public record ResendAccountConfirmCommand : IRequest
    {
        public string Username { get; set; } = default!;
    }

    /// <summary>
    /// Handler for <see cref="ResendAccountConfirmCommand"/>
    /// </summary>
    public class ResendAccountConfirmCommandHandler : IRequestHandler<ResendAccountConfirmCommand>
    {
        private readonly IEmailPatternService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResendAccountConfirmCommandHandler(
            UserManager<ApplicationUser> userManager, IEmailPatternService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        /// <summary>
        /// Resends account confirmation message with a link to the user's email
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <summary>
        /// If the user does not exists throws <see cref="NotFoundException"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException">
        /// Thrown when user with the provided username was not found
        /// </exception>
        public async Task Handle(ResendAccountConfirmCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new NotFoundException($"User with username {request.Username} does not exist");
            }

            await _emailService.SendAccountConfirmEmailAsync(user);
        }

    }

}
