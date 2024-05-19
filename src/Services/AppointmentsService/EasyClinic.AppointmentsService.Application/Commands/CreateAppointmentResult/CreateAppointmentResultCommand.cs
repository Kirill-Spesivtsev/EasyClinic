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
    /// Command for creating new AppointmentResult.
    /// </summary>
    public record CreateAppointmentResultCommand : IRequest<AppointmentResult>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AppointmentId { get; set; }

        public string Complaints { get; set; } = null!;

        public string Conclusion { get; set; } = null!;

        public string Recommendations { get; set; } = null!;

        [JsonIgnore]
        public string? Email { get; set; } = null!;

        [JsonIgnore]
        public string? UserName { get; set; } = null!;
    }

    /// <summary>
    /// Command handler for <see cref="CreateAppointmentResultCommand"/>
    /// </summary>
    public class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand, AppointmentResult>
    {
        private readonly IUnitOfWork _repository;
        private readonly IEmailPatternService _emailPatternService;
        private readonly IMapper _mapper;

        public CreateAppointmentResultCommandHandler( IMapper mapper,
            IEmailPatternService emailPatternService,
            IUnitOfWork repository)
        {
            _emailPatternService = emailPatternService;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new AppointmentResult.
        /// </summary>
        /// <returns>Created <see cref="Appointment"/> instance</returns>
        public async Task<AppointmentResult> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var appointmentResult = _mapper.Map<CreateAppointmentResultCommand, AppointmentResult>(request);

            var appointment = await _repository.Appointments.GetByIdAsync(request.AppointmentId);

            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.AppointmentId);
            }
            
            var sentEmail = _emailPatternService.SendAppointmentResultsEmail(
                appointmentResult,
                appointment.Date, appointment.Time,
                email: request.Email!, username: request.UserName!);

            _repository.AppointmentResults.Add(appointmentResult);
            await _repository.SaveChangesAsync();

            await sentEmail;

            return appointmentResult;
        }

    }
}
