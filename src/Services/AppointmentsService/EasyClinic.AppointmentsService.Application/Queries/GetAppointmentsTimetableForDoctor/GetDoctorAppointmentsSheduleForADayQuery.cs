using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace EasyClinic.AppointmentsService.Application.Queries;


/// <summary>
/// Query to Get doctor appointments schedule for a day.
/// </summary>
public record GetDoctorAppointmentsSheduleForADayQuery : IRequest<List<Appointment>> 
{ 
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public Guid DoctorId { get; set; }
};

/// <summary>
/// Handler for <see cref="GetDoctorAppointmentsSheduleForADayQuery"/>
/// </summary>
public class GetDoctorAppointmentsSheduleForADayQueryHandler : IRequestHandler<GetDoctorAppointmentsSheduleForADayQuery, List<Appointment>>
{
    private readonly IUnitOfWork _repository;

    public GetDoctorAppointmentsSheduleForADayQueryHandler(IUnitOfWork repository)
    {
         _repository = repository;
    }

    /// <summary>
    /// Retrieves doctor appointments shedule for a day for doctor.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>s
    /// <returns></returns>
    public async Task<List<Appointment>> Handle(GetDoctorAppointmentsSheduleForADayQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Appointments.GetAllAsync(
            x => request.Date == x.Date
            && x.DoctorId == request.DoctorId);

        return result;
    }
}
