using AutoMapper;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using EasyClinic.AppointmentsService.Domain.Contracts;
using FluentValidation;
using EasyClinic.AppointmentsService.Domain.Exceptions;
using EasyClinic.AppointmentsService.Application.Services;
using System.Text.Json.Serialization;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    /// <summary>
    /// Command for resheduling (updating) an Appointment.
    /// </summary>
    public record ResheduleAppointmentCommand : IRequest<Appointment>
    {

        public Guid Id { get; set; }

        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = null!;

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
    }

    /// <summary>
    /// Command handler for <see cref="ResheduleAppointmentCommand"/>
    /// </summary>
    public class ResheduleAppointmentCommandHandler : IRequestHandler<ResheduleAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _repository;
        private readonly IEmailPatternService _emailPatternService;
        private readonly IMapper _mapper;

        public ResheduleAppointmentCommandHandler( IMapper mapper,
            IEmailPatternService emailPatternService,
            IUnitOfWork repository)
        {
            _emailPatternService = emailPatternService;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Reshedules (updates) and Appointment.
        /// </summary>
        /// <returns>Created <see cref="Appointment"/> instance</returns>
        public async Task<Appointment> Handle(ResheduleAppointmentCommand request, CancellationToken cancellationToken)
        {

            var appointment = await _repository.Appointments.GetByIdAsync(request.Id);

            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            if (appointment.IsApproved)
            {
                throw new BadRequestException("You cannot reshedule the approved appointment");
            }

            if (appointment.Date.ToDateTime(appointment.Time) <= DateTime.Now)
            {
                throw new BadRequestException("You cannot set an appointment in the past");
            }

            appointment.DoctorId = request.DoctorId;
            appointment.DoctorFullName = request.DoctorFullName;
            appointment.Date = request.Date;
            appointment.Time = request.Time;

            _repository.Appointments.Update(appointment);
            await _repository.SaveChangesAsync();

            return appointment;
        }

    }
}
