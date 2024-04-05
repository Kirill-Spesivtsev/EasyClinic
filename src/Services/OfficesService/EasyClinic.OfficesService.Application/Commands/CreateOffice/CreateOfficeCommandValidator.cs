using EasyClinic.OfficesService.Application.Commands;
using EasyClinic.OfficesService.Application.DTO;
using FluentValidation;


namespace EasyClinic.OfficesService.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="CreateOfficeCommand"/>
    /// </summary>
    public sealed class CreateOfficeCommandValidator : AbstractValidator<OfficeDto>
    {
        private const string PhoneRegex = @"^\+?[0-9]{1,4}?[-.\s]?\(?[0-9]{1,3}?\)?[-.\s]?[0-9]{1,4}[-.\s]?[0-9]{1,4}[-.\s]?[0-9]{1,9}$";

        public CreateOfficeCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the City.");

            RuleFor(x => x.Street)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the Street.");

            RuleFor(x => x.HouseNumber)
                .NotNull()
                .GreaterThan(0).WithMessage("Please, enter the positive House Number.");

            RuleFor(x => x.RegistryPhone)
                .NotNull()
                .NotEmpty().WithMessage("Please, enter the registry phone.")
                .Matches(PhoneRegex).WithMessage("Please, enter a valid phone number.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Choose a valid office status.");
        }

    }
}
