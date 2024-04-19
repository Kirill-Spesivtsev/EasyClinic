using EasyClinic.ServicesService.Application.DTO;
using FluentValidation;

namespace EasyClinic.ServicesService.Application.Validators;

public class PatientProfileDtoValidator : AbstractValidator<ServiceDto>
{
    public PatientProfileDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Choose a valid office status.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.SpecializationId)
            .NotEmpty().WithMessage("Specialization ID is required.");
    }

}