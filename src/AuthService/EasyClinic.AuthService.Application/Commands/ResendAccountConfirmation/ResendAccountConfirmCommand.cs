using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyClinic.AuthService.Application.Commands.ResendAccountConfirmation
{
    public record ResendAccountConfirmCommand : IRequest
    {
        public string Username { get; set; } = default!;
    }

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
