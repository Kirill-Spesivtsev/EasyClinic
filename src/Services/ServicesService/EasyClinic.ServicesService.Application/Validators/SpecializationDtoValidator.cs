using EasyClinic.ServicesService.Application.DTO;
using FluentValidation;

namespace EasyClinic.ServicesService.Application.Validators;

public class SpeciazlizationDtoValidator : AbstractValidator<SpecializationDto>
{
    public SpeciazlizationDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Choose a valid office status.");
    }

}