using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace EasyClinic.AppointmentsService.Application.Queries;


/// <summary>
/// Query to Get all Appointments for Doctor.
/// </summary>
public record GetAppointmentsHistoryForDoctorQuery : IRequest<List<Appointment>> 
{ 
    public Guid DoctorId { get; set; }
};

/// <summary>
/// Handler for <see cref="GetAppointmentsHistoryForDoctorQuery"/>
/// </summary>
public class GetAppointmentsHistoryForDoctorQueryHandler : IRequestHandler<GetAppointmentsHistoryForDoctorQuery, List<Appointment>>
{
    private readonly IUnitOfWork _repository;

    public GetAppointmentsHistoryForDoctorQueryHandler(IUnitOfWork repository)
    {
         _repository = repository;
    }

    /// <summary>
    /// Retrieves all Appointments for Doctor.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>s
    /// <returns></returns>
    public async Task<List<Appointment>> Handle(GetAppointmentsHistoryForDoctorQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Appointments.GetAllAsync(x => x.DoctorId == request.DoctorId);

        return result;
    }
}
