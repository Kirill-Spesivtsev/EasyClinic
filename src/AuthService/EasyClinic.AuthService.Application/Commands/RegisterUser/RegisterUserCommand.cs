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
        private readonly ITokenService _tokenService;
        private readonly IEmailPatternService _emailService;

        public RegisterUserCommandHandler(
            IRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IEmailPatternService emailService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _userRepository = userRepository;
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
                var registration = await _userManager.CreateAsync(user, request.Password);
                var rolesTask = _userManager.GetRolesAsync(user);

                if (request.Password != request.RepeatPassword)
                {
                    throw new BadRequestException("Passwords do not match");
                }

                if (!registration.Succeeded)
                {
                    throw new BadRequestException("Invalid data provided");
                }

                await _emailService.SendAccountConfirmEmailAsync(user);

                var result = new UserToReturnDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Username = user.UserName!,
                    Token = _tokenService.GenerateJwtToken(user, await rolesTask),
                };

                transaction.Commit();

                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}
