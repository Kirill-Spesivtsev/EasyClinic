using EasyClinic.AuthService.Application.DTO;
using FluentValidation;
using MediatR;

namespace EasyClinic.AuthService.Application.Commands
{
    public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the email")
                .EmailAddress().WithMessage("You've entered an invalid email");
            
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the password");
        }

    }
}
