using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery : IRequest<UserToReturnDto>
    {
        public ClaimsPrincipal User { get; set; } = default!;
    };

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserToReturnDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public GetCurrentUserQueryHandler(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserToReturnDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);

            var result = new UserToReturnDto
            {
                Id = user?.Id!,
                Email = user?.Email!,
                Username = user?.UserName!,
                Token = _tokenService.GenerateJwtToken(user!),
            };

            return result;
        }
    }
}
