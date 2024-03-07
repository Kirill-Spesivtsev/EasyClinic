using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using EasyClinic.AuthService.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static System.Data.IsolationLevel;

namespace EasyClinic.AuthService.Application.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest<UserToReturnDto>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string RepeatPassword { get; set; } = default!;
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserToReturnDto>
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailPatternService _emailService;

        public RegisterUserCommandHandler(
            IRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IEmailPatternService emailService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _userRepository = userRepository;
            _roleManager = roleManager;
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

            using var transaction = _userRepository.BeginTransaction(ReadCommitted);

            try
            {
                var register = await _userManager.CreateAsync(user, request.Password);

                if (request.Password != request.RepeatPassword)
                {
                    throw new BadRequestException("Passwords do not match");
                }

                if (!register.Succeeded)
                {
                    throw new BadRequestException("Invalid data provided");
                }

                await _emailService.SendAccountConfirmEmailAsync(user);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            var result = new UserToReturnDto
            {
                Id = user.Id,
                Email = user.Email!,
                Username = user.UserName!,
                Token = _tokenService.GenerateJwtToken(user),
            };

            return result;
        }

    }
}
