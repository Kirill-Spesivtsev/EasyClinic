using EasyClinic.AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Queries.CheckEmailExistence
{
    public record CheckEmailExistenceQuery : IRequest<bool>
    {
        public string Email { get; set; } = default!;
    }

    public class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckEmailExistenceQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(request.Email);
            return result != null;
        }
    }
}
