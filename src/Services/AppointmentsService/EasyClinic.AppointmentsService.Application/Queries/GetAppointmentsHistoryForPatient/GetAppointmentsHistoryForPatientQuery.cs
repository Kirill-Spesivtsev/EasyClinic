using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace EasyClinic.AppointmentsService.Application.Queries;


/// <summary>
/// Query to Get all Appointments for Patient.
/// </summary>
public record GetAppointmentsHistoryForPatientQuery : IRequest<List<Appointment>> 
{ 
    public Guid PatientId { get; set; }
};

/// <summary>
/// Handler for <see cref="GetAppointmentsHistoryForPatientQuery"/>
/// </summary>
public class GetAppointmentsHistoryForPatientQueryHandler : IRequestHandler<GetAppointmentsHistoryForPatientQuery, List<Appointment>>
{
    private readonly IUnitOfWork _repository;

    public GetAppointmentsHistoryForPatientQueryHandler(IUnitOfWork repository)
    {
         _repository = repository;
    }

    /// <summary>
    /// Retrieves all Appointments for Patient.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Appointment>> Handle(GetAppointmentsHistoryForPatientQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Appointments.GetAllAsync(x => x.PatientId == request.PatientId);

        return result;
    }
}
