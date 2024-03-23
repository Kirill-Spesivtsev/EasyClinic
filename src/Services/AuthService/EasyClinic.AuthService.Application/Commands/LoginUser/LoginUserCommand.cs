using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyClinic.AuthService.Application.Commands.LoginUser
{
    /// <summary>
    /// Command for getting user JWT token and other data.
    /// </summary>
    public record LoginUserCommand : IRequest<UserToReturnDto>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }

    /// <summary>
    /// Handler for <see cref="LoginUserCommand"/>
    /// </summary>
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

        /// <summary>
        /// Gets user JWT token and other data if credentials are valid.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User JWT token and other data</returns>
        /// <exception cref="NotFoundException">
        /// Thrown if user with such email does not exist
        /// </exception>
        /// <exception cref="UnauthorizedException">
        /// 
        /// </exception>
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
