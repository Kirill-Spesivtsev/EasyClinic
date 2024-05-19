using AutoMapper;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using EasyClinic.AppointmentsService.Domain.Contracts;
using FluentValidation;
using EasyClinic.AppointmentsService.Domain.Exceptions;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    /// <summary>
    /// Command for approving Appointment (by Receptionist or higher).
    /// </summary>
    public record ApproveAppointmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Command handler for <see cref="ApproveAppointmentCommand"/>
    /// </summary>
    public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand>
    {
        private readonly IRepository<Appointment> _appointmentsRepository;

        public ApproveAppointmentCommandHandler(
            IRepository<Appointment> appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        /// <summary>
        /// Approves Appointment (by Receptionist or higher).
        /// </summary>
        public async Task Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetByIdAsync(request.Id);
            
            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            appointment.IsApproved = true;

            await _appointmentsRepository.SaveChangesAsync();
        }

    }
}
