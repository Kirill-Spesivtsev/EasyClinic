﻿using EasyClinic.AuthService.Application.DTO;
using MediatR;

namespace EasyClinic.AuthService.Application.Commands
{
    public record RegisterUserCommand : IRequest<UserToReturnDto>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string RepeatPassword { get; set; } = default!;
    }
}
