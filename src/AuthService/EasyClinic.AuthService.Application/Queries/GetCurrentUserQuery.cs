using EasyClinic.AuthService.Application.DTO;
using MediatR;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Queries
{
    public record GetCurrentUserQuery : IRequest<UserToReturnDto>
    {
        public ClaimsPrincipal User { get;set; } = default!;
    };
}
