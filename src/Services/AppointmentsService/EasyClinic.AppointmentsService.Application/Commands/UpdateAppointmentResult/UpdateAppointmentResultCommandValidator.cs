using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class UpdateAppointmentResultCommandValidator : AbstractValidator<UpdateAppointmentResultCommand>
    {
        public UpdateAppointmentResultCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("Appointment Id is required");

            RuleFor(x => x.Complaints).NotNull().WithMessage("Complaints are required.");

            RuleFor(x => x.Conclusion).NotNull().WithMessage("Conclusion is required.");

            RuleFor(x => x.Recommendations).NotNull().WithMessage("Recomendations are required.");
        }
    }
}
