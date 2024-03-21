﻿using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyClinic.AuthService.Application.Commands.LoginUser
{
    public record LoginUserCommand : IRequest<UserToReturnDto>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserToReturnDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserToReturnDto> Handle(LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("User with such email does not exist");
            }

            var rolesTask = _userManager.GetRolesAsync(user);

            var login = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!login.Succeeded)
            {
                throw new UnauthorizedException("Invalid login credentials");
            }

            var result = new UserToReturnDto
            {
                Id = user.Id,
                Email = user.Email!,
                Username = user.UserName!,
                Token = _tokenService.GenerateJwtToken(user, await rolesTask),
            };

            return result;
        }

    }

}
