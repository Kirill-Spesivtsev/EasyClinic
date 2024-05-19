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
    public record UpdateAppointmentResultCommand : IRequest<AppointmentResult>
    {
        public Guid Id { get; set; }

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
    /// Command handler for <see cref="UpdateAppointmentResultCommand"/>
    /// </summary>
    public class UpdateAppointmentResultCommandHandler : IRequestHandler<UpdateAppointmentResultCommand, AppointmentResult>
    {
        private readonly IUnitOfWork _repository;
        private readonly IEmailPatternService _emailPatternService;
        private readonly IMapper _mapper;

        public UpdateAppointmentResultCommandHandler( IMapper mapper,
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
        public async Task<AppointmentResult> Handle(UpdateAppointmentResultCommand request, CancellationToken cancellationToken)
        {

            var appointmentResult = await _repository.AppointmentResults.GetByIdAsync(request.AppointmentId);

            if (appointmentResult == null)
            {
                throw new NotFoundException("Appointment Result", request.AppointmentId);
            }

            _mapper.Map(request, appointmentResult);

            _repository.AppointmentResults.Update(appointmentResult);
            await _repository.SaveChangesAsync();

            await _emailPatternService.SendAppointmentResultsEmail(appointmentResult,
                appointmentResult.Appointment.Date, appointmentResult.Appointment.Time,
                email: request.Email!, username: request.UserName!);

            return appointmentResult;
        }

    }
}
