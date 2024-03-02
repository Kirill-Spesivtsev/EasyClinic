using EasyClinic.AuthService.Application.DTO;
using MediatR;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Commands
{
    public record ChangePasswordSubmitCommand : IRequest
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;

        public string NewPass { get; set; } = default!;
    };
}
