using EasyClinic.AuthService.Application.DTO;
using MediatR;

namespace EasyClinic.AuthService.Application.Commands
{
    public record SendPasswordResetCommand : IRequest
    {
        public string Username { get; set; } = default!;
    }

}
