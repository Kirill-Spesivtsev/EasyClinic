using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Commands.ChangePasswordSubmit
{
    public record ChangePasswordSubmitCommand : IRequest
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string NewPass { get; set; } = default!;
    };

    public class ChangePasswordSubmitCommandHandler : IRequestHandler<ChangePasswordSubmitCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordSubmitCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Handle(ChangePasswordSubmitCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User with id {request.UserId} does not exist");
            }

            var tokenCheck = await _userManager.ResetPasswordAsync(
                user, request.Token, request.NewPass);

            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
