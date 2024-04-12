
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries;

/// <summary>
/// Query to Get all Patient Profiles.
/// </summary>
public record GetAllPatientProfilesQuery : IRequest<List<PatientProfile>> { };

/// <summary>
/// Handler for <see cref="GetAllPatientProfilesQuery"/>
/// </summary>
public class GetAllPatientProfilesQueryHandler : IRequestHandler<GetAllPatientProfilesQuery, List<PatientProfile>>
{
    private readonly IRepository<PatientProfile> _profilesRepository;

    public GetAllPatientProfilesQueryHandler(IRepository<PatientProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves all Patient Profiles.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<PatientProfile>> Handle(GetAllPatientProfilesQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository.GetAllAsync();
    }
}
