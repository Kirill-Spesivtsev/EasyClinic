using EasyClinic.AuthService.Application.Commands;
using EasyClinic.AuthService.Application.DTO;
using FluentValidation;
using MediatR;

namespace EasyClinic.AuthService.Application.Validators
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the email")
                .EmailAddress().WithMessage("You've entered an invalid email");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty().WithMessage("Password should not be empty")
                .MinimumLength(6).WithMessage("Password should have at least 6 symbols")
                .MaximumLength(15).WithMessage("Password should not be longer than 15 symbols");

            RuleFor(x => x.RepeatPassword)
                .NotNull()
                .Equal(x => x.Password).WithMessage("The passwords you’ve entered don’t coincide");
        }

    }
}
