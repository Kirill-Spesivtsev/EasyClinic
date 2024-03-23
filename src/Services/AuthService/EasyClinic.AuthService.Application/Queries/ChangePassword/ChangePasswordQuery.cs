using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Queries.ChangePassword
{
    /// <summary>
    /// Query of get link sent to email for password change.
    /// </summary>
    public record ChangePasswordQuery : IRequest
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
    };

    /// <summary>
    /// Handler for <see cref="ChangePasswordQuery"/>
    /// </summary>
    public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Checks if user exists.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">
        /// Thrown if user does not exist.
        /// </exception>
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
