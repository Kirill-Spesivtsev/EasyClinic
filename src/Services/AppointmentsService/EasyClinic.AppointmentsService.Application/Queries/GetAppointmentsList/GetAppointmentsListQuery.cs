using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace EasyClinic.AppointmentsService.Application.Queries;

public enum AppointmentStatus: byte
{
    All = 0,
    Approved = 1,
    NotApproved = 2
}

/// <summary>
/// Query to Get filtered list of all appointments.
/// </summary>
public record GetAppointmentsListQuery : IRequest<List<Appointment>> 
{ 
    public DateOnly? Date { get; set; } = null!;
    public string? DoctorFullName { get; set; } = null!;

    public string? ServiceName { get; set; } = null!;

    public string? PatientFullName { get; set; } = null!;

    public string? OfficeName { get; set; } = null!;

    public AppointmentStatus Status { get; set; } = AppointmentStatus.All;
};

/// <summary>
/// Handler for <see cref="GetAppointmentsListQuery"/>
/// </summary>
public class GetAppointmentsListQueryHandler : IRequestHandler<GetAppointmentsListQuery, List<Appointment>>
{
    private readonly IUnitOfWork _repository;

    public GetAppointmentsListQueryHandler(IUnitOfWork repository)
    {
         _repository = repository;
    }

    /// <summary>
    /// Retrieves all appointments filtered by query.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>s
    /// <returns></returns>
    public async Task<List<Appointment>> Handle(GetAppointmentsListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Appointments.GetAllAsync(
            x => (request.Date == null || request.Date == x.Date)  
                && (request.Status == AppointmentStatus.All 
                    || (request.Status == AppointmentStatus.Approved) && x.IsApproved 
                    || (request.Status == AppointmentStatus.NotApproved) && !x.IsApproved)
                && (request.DoctorFullName == null || x.DoctorFullName == request.DoctorFullName)
                && (request.ServiceName == null || x.ServiceName == request.ServiceName)
                && (request.PatientFullName == null || x.PatientFullName == request.PatientFullName)
                && (request.OfficeName == null || x.OfficeName == request.OfficeName));

        return result;
    }
}
