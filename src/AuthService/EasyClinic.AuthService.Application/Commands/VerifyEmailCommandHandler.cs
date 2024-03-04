using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EasyClinic.AuthService.Domain.Exceptions;
using System.Web;
using System.Text;
using Microsoft.Extensions.Logging;

namespace EasyClinic.AuthService.Application.Commands
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChangePasswordSubmitCommandHandler> _logger;

        public VerifyEmailCommandHandler(UserManager<ApplicationUser> userManager, ILogger<ChangePasswordSubmitCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User with id {request.UserId} does not exist");
            }

            var tokenCheck = await _userManager.ConfirmEmailAsync(
                user, request.Token);

            _logger.LogInformation(request.Token);
            _logger.LogInformation(HttpUtility.UrlDecode(request.Token, Encoding.ASCII));
            _logger.LogInformation(HttpUtility.UrlDecode(request.Token, Encoding.UTF8));


            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
