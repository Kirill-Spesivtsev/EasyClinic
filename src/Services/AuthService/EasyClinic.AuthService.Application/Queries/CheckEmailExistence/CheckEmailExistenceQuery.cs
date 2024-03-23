using EasyClinic.AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Queries.CheckEmailExistence
{
    /// <summary>
    /// Query to Check if email exists
    /// </summary>
    public record CheckEmailExistenceQuery : IRequest<bool>
    {
        public string Email { get; set; } = default!;
    }

    /// <summary>
    /// Handler for <see cref="CheckEmailExistenceQuery"/>
    /// </summary>
    public class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckEmailExistenceQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Checks if the email exists.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if email exists, false otherwise</returns>
        public async Task<bool> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(request.Email);
            return result != null;
        }
    }
}
