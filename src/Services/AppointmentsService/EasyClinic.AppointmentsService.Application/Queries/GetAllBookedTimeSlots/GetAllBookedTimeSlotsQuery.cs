using EasyClinic.AppointmentsService.Application.DTO;
using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;
using MassTransit;
using MassTransit.Transports;
using MassTransitData.Messages;
using MediatR;

namespace EasyClinic.AppointmentsService.Application.Queries;

/// <summary>
/// Query to Get all time slots.
/// </summary>
public record GetAllBookedTimeSlotsQuery : IRequest<List<TimeSlotDto>> 
{ 
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? DoctorId { get; set; }

    public Guid ServiceId { get; set; }

    public Guid PatientId { get; set; }

    public DateOnly Date { get; set; }
};

/// <summary>
/// Handler for <see cref="GetAllBookedTimeSlotsQuery"/>
/// </summary>
public class GetAllBookedTimeSlotsQueryHandler : IRequestHandler<GetAllBookedTimeSlotsQuery, List<TimeSlotDto>>
{
    private readonly IRepository<Appointment> _appointmentsRepository;
    private readonly IBus _bus;

    public GetAllBookedTimeSlotsQueryHandler( IBus bus,
        IRepository<Appointment> appointmentsRepository)
    {
        _bus = bus;
        _appointmentsRepository = appointmentsRepository;
    }

    /// <summary>
    /// Retrieves all time slots for specific day and service.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<TimeSlotDto>> Handle(GetAllBookedTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        var timeSlotSize = await _bus.Request<GuidMessage, IntMessage>(new GuidMessage 
        {
            Value = request.ServiceId
        });

        DateTime nearestPossibleTime = RoundTimeToNearestTimeSlot(
            DateTime.Now, TimeSpan.FromMinutes(10 * timeSlotSize.Message.Value));

        DateTime startWorkingTime = DateTime.MinValue.AddHours(9);
        DateTime endWorkingTime = DateTime.MinValue.AddHours(20);

        TimeOnly startTime = nearestPossibleTime > startWorkingTime
            ? TimeOnly.FromDateTime(nearestPossibleTime)
            : TimeOnly.FromDateTime(startWorkingTime);

        TimeOnly endTime = TimeOnly.FromDateTime(
            endWorkingTime.AddMinutes(-10 * timeSlotSize.Message.Value));

        var occupiedTimeSlots = await _appointmentsRepository.GetAllAsync(x => 
            x.DoctorId == request.DoctorId &&
            x.ServiceId == request.ServiceId &&
            x.Time >= startTime &&
            x.Time <= endTime
        );

        return occupiedTimeSlots.Select(x => new TimeSlotDto{Time = x.Time, IsAvailable = false}).ToList();
    }

    private static DateTime RoundTimeToNearestTimeSlot(DateTime dt, TimeSpan d)
    {
        dt.Add(d);
        var delta = dt.Ticks % d.Ticks;
        bool roundUp = delta > d.Ticks / 2;
        var offset = roundUp ? d.Ticks : 0;
        return new DateTime(dt.Ticks + offset - delta, dt.Kind);
    }
}
