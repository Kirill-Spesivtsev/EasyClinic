using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EasyClinic.AuthService.Domain.Exceptions;
using System.Web;
using System.Text;

namespace EasyClinic.AuthService.Application.Commands
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User with id {request.UserId} does not exist");
            }

            var tokenCheck = await _userManager.ConfirmEmailAsync(
                user, HttpUtility.UrlDecode(request.Token, Encoding.UTF8));

            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
