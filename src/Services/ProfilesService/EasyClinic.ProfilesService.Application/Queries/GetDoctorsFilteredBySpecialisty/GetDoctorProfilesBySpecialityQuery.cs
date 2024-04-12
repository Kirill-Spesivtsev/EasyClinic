using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries;

/// <summary>
/// Query to Get all doctor profiles.
/// </summary>
public record GetDoctorProfilesBySpecialityQuery : IRequest<List<DoctorProfile>> 
{
    public string SpecializationName { get; set; } = default!;
};

/// <summary>
/// Handler for <see cref="GetAllDoctorProfilesQuery"/>
/// </summary>
public class GetDoctorProfilesBySpecialityQueryHandler : IRequestHandler<GetDoctorProfilesBySpecialityQuery, List<DoctorProfile>>
{
    private readonly IDoctorProfilesRepository _profilesRepository;

    public GetDoctorProfilesBySpecialityQueryHandler(IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves all doctor profiles.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<DoctorProfile>> Handle(GetDoctorProfilesBySpecialityQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository
            .GetFilteredAsync(x => x.MedicalSpecialization.Name == request.SpecializationName);
    }
}
