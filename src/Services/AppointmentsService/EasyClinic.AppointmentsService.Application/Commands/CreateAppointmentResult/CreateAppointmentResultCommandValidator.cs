using System;
using FluentValidation;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    public class CreateAppointmentResultCommandValidator : AbstractValidator<CreateAppointmentResultCommand>
    {
        public CreateAppointmentResultCommandValidator()
        {
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("Appointment Id is required");
            
            RuleFor(x => x.Complaints).NotNull().WithMessage("Complaints are required.");

            RuleFor(x => x.Conclusion).NotNull().WithMessage("Conclusion is required.");

            RuleFor(x => x.Recommendations).NotNull().WithMessage("Recomendations are required.");
        }
    }
}
