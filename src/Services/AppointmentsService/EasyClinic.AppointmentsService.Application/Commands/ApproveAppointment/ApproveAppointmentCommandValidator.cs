using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class ApproveAppointmentCommandValidator : AbstractValidator<ApproveAppointmentCommand>
    {
        public ApproveAppointmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Appointment Id is required.");
        }
    }
}
