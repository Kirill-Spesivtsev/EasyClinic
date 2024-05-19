using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class CancelAppointmentCommandValidator : AbstractValidator<CancelAppointmentCommand>
    {
        public CancelAppointmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
