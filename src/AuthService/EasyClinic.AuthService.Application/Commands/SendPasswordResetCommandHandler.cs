using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using EasyClinic.AuthService.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Commands
{
    public class SendPasswordResetCommandHandler : IRequestHandler<SendPasswordResetCommand>
    {
        private readonly IEmailPatternService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SendPasswordResetCommandHandler(
            UserManager<ApplicationUser> userManager, IEmailPatternService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task Handle(SendPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new NotFoundException($"User with username {request.Username} does not exist");
            }

            await _emailService.SendPasswordChangeEmailAsync(user);
        }

    }

}
