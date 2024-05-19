using AutoMapper;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using EasyClinic.AppointmentsService.Domain.Contracts;
using FluentValidation;
using EasyClinic.AppointmentsService.Domain.Exceptions;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    /// <summary>
    /// Command for cancelling an Appointment.
    /// </summary>
    public record CancelAppointmentCommand : IRequest<Appointment>
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Command handler for <see cref="CancelAppointmentCommand"/>
    /// </summary>
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _repository;

        public CancelAppointmentCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cancels an Appointment by Id.
        /// </summary>
        /// <returns></returns>
        public async Task<Appointment> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {

            var appointment = await _repository.Appointments.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            if (appointment.Date.ToDateTime(appointment.Time) <= DateTime.Now)
            {
                throw new BadRequestException("You cannot cancel past appointments");
            }

            _repository.Appointments.Delete(appointment);
            await _repository.AppointmentResults.SaveChangesAsync();
            return appointment;
        }

    }
}
