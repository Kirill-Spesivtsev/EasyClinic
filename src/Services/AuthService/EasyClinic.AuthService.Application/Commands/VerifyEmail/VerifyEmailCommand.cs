using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace EasyClinic.AuthService.Application.Commands.VerifyEmail
{
    public record VerifyEmailCommand : IRequest
    {
        public string UserId { get; set; } = default!;

        public string Token { get; set; } = default!;
    };

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
            var token = HttpUtility.UrlDecode(request.Token).Replace(" ", "+");
            var tokenCheck = await _userManager.ConfirmEmailAsync(user, token);


            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
