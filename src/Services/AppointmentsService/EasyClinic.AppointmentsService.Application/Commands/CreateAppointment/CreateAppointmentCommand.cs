using AutoMapper;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using EasyClinic.AppointmentsService.Domain.Contracts;
using FluentValidation;
using EasyClinic.AppointmentsService.Domain.Exceptions;
using System.Text.Json.Serialization;

namespace EasyClinic.AppointmentsService.Application.Commands
{
    /// <summary>
    /// Command for creating new Appointment.
    /// </summary>
    public record CreateAppointmentCommand : IRequest<Appointment>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = null!;

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;

        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; } = null!;
        public string PatientPhone { get; set; } = null!;

        [JsonIgnore]
        public string? PatientEmail { get; set; } = null!;
        [JsonIgnore]
        public string? PatientUserName { get; set; } = null!;

        public Guid OfficeId { get; set; }
        public string OfficeName { get; set; } = null!;

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
    }

    /// <summary>
    /// Command handler for <see cref="CreateAppointmentCommand"/>
    /// </summary>
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(
            IMapper mapper,
            IUnitOfWork repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new Appointment.
        /// </summary>
        /// <returns>Created <see cref="Appointment"/> instance</returns>
        public async Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {

            var appointment = _mapper.Map<CreateAppointmentCommand, Appointment>(request);

            _repository.Appointments.Add(appointment);
            await _repository.Appointments.SaveChangesAsync();
            return appointment;
        }

    }
}
