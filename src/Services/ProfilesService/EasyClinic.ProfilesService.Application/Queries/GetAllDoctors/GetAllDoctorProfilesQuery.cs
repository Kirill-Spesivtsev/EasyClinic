
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries;

/// <summary>
/// Query to Get all doctor profiles.
/// </summary>
public record GetAllDoctorProfilesQuery : IRequest<List<DoctorProfile>> { };

/// <summary>
/// Handler for <see cref="GetAllDoctorProfilesQuery"/>
/// </summary>
public class GetAllDoctorProfilesQueryHandler : IRequestHandler<GetAllDoctorProfilesQuery, List<DoctorProfile>>
{
    private readonly IDoctorProfilesRepository _profilesRepository;

    public GetAllDoctorProfilesQueryHandler(IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves all Doctor Profiles.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<DoctorProfile>> Handle(GetAllDoctorProfilesQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository.GetAllAsync();
    }
}
