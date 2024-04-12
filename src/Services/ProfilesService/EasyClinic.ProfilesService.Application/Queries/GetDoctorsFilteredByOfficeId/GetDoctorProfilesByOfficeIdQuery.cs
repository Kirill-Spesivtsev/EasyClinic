using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries;

/// <summary>
/// Query to Get all doctor profiles.
/// </summary>
public record GetDoctorProfilesByOffieIdQuery : IRequest<List<DoctorProfile>> 
{
    public Guid OfficeId { get; init; }
};

/// <summary>
/// Handler for <see cref="GetAllDoctorProfilesQuery"/>
/// </summary>
public class GetDoctorProfilesByOffieIdQueryHandler : IRequestHandler<GetDoctorProfilesByOffieIdQuery, List<DoctorProfile>>
{
    private readonly IDoctorProfilesRepository _profilesRepository;

    public GetDoctorProfilesByOffieIdQueryHandler(IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves all Doctor Profiles filtered by Office id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<DoctorProfile>> Handle(GetDoctorProfilesByOffieIdQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository.GetFilteredAsync(x => x.OfficeId == request.OfficeId);
    }
}
