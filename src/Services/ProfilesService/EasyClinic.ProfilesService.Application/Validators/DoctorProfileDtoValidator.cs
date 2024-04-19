using EasyClinic.ProfilesService.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.Commands.CreateDoctorProfile;

public class DoctorProfileDtoValidator : AbstractValidator<DoctorProfileDto>
{    
    private const string EmailRegexPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public DoctorProfileDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100).WithMessage("Middle name must not exceed 50 characters.");

        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeValidDate).WithMessage("Invalid date of birth.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegexPattern).WithMessage("Invalid email address.");

        RuleFor(x => x.OfficeId)
            .NotEmpty().WithMessage("Office ID is required.");

        RuleFor(x => x.CareerStartYear)
            .NotEmpty().WithMessage("Career start year is required.")
            .Must(BeValidYear).WithMessage("Invalid career start year.");

        RuleFor(x => x.MedicalSpecializationId)
            .NotEmpty().WithMessage("Medical specialization ID is required.");

        RuleFor(x => x.EmployeeStatusId)
            .NotEmpty().WithMessage("Employee status ID is required.");
    }

    private bool BeValidDate(DateOnly date) => date <= DateOnly.FromDateTime(DateTime.Today);

    private bool BeValidYear(string year)
    {
        string format = "yyyy";

        if (DateTime.TryParseExact(year, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
        {
            return result.Year <= DateTime.Now.Year;
        }

        return false;
    }
}
