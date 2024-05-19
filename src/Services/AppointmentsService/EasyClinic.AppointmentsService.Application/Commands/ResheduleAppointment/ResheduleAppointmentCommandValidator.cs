using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class ResheduleAppointmentCommandValidator: AbstractValidator<ResheduleAppointmentCommand>
    {
        public ResheduleAppointmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Appointment Id is required");

            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("Doctor Id is required");

            RuleFor(x => x.DoctorFullName).NotEmpty().WithMessage("Doctor full name is required");

            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.Time).NotEmpty().WithMessage("Time is required");

        }
    }
}
