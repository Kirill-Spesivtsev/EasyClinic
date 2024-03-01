using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using EasyClinic.AuthService.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserToReturnDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserToReturnDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
           var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var userCheck = await _userManager.FindByEmailAsync(request.Email);

            if (userCheck != null)
            {
                throw new ConflictException("User with such email already exists");
            }

            var register = await _userManager.CreateAsync(user, request.Password);

            if (request.Password != request.RepeatPassword) 
            { 
                throw new BadRequestException("Passwords do not match");
            } 

            if (!register.Succeeded) 
            { 
                throw new BadRequestException("Invalid data provided");
            } 

            var result = new UserToReturnDto
            {
                Id = user.Id,
                Email = user.Email!,
                Token = _tokenService.GenerateToken(user),
            };

            return result;
        }

    }
}
