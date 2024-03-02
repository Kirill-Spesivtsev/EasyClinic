using EasyClinic.AuthService.Application.DTO;
using MediatR;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Queries
{
    public record ChangePasswordQuery : IRequest
    {
        public string UserId { get;set; } = default!;
        public string Token { get;set; } = default!;
    };
}
