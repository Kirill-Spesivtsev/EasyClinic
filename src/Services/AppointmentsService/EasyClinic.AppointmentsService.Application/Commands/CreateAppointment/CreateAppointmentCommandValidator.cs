using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("Doctor ID is required.");
            RuleFor(x => x.DoctorFullName).NotEmpty().WithMessage("Doctor Full Name Id is required.");

            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service ID is required.");
            RuleFor(x => x.ServiceName).NotEmpty().WithMessage("ServiceName is required.");

            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient ID is required.");
            RuleFor(x => x.PatientFullName).NotEmpty().WithMessage("PatientFullName is required.");

            RuleFor(x => x.OfficeId).NotEmpty().WithMessage("Office ID is required.");
            RuleFor(x => x.OfficeName).NotEmpty().WithMessage("Office Name is required.");

            RuleFor(x => x.Date).NotEmpty().WithMessage("DateTime is required.");

            RuleFor(x => x.Time).NotEmpty().WithMessage("DateTime is required.");
        }
    }
}
