using EasyClinic.AuthService.Application.Queries;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EasyClinic.AuthService.Application.Commands
{
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
                user, HttpUtility.UrlDecode(request.Token), request.NewPass);

            if (!tokenCheck.Succeeded)
            {
                throw new BadRequestException("Invalid token provided");
            }

        }
    }
}
