using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EasyClinic.AuthService.Domain.Exceptions;
using EasyClinic.AuthService.Application.Queries;

namespace EasyClinic.AuthService.Application.QueryHandlers
{
    public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User with id {request.UserId} does not exist");
            }
        }
    }
}
