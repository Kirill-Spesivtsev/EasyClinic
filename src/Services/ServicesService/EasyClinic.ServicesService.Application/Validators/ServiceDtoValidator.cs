﻿using EasyClinic.ServicesService.Application.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ServicesService.Application.Validators;

public class PatientProfileDtoValidator : AbstractValidator<ServiceDto>
{
    public PatientProfileDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Choose a valid office status.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.SpecializationId)
            .NotEmpty().WithMessage("Specialization ID is required.");
    }

}