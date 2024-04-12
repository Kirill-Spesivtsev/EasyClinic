using EasyClinic.ProfilesService.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.Commands.CreateDoctorProfile;

public class ReceptionistProfileDtoValidator : AbstractValidator<ReceptionistProfileDto>
{
    private const string EmailRegexPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public ReceptionistProfileDtoValidator()
    {RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100).WithMessage("Middle name must not exceed 50 characters.");

        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegexPattern).WithMessage("Invalid email address.");

        RuleFor(x => x.OfficeId)
            .NotEmpty().WithMessage("Office ID is required.");

    }
}
