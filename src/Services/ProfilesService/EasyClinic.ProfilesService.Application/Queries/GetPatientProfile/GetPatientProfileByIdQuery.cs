using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using EasyClinic.ProfilesService.Domain.Contracts;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Queries;

/// <summary>
/// Query to Get <see cref="PatientProfile"/> by Id.
/// </summary>
public record GetPatientProfileByIdQuery : IRequest<PatientProfile>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Handler for <see cref="GetPatientProfileByIdQuery"/>
/// </summary>
public class GetPatientProfileByIdQueryHandler : IRequestHandler<GetPatientProfileByIdQuery, PatientProfile>
{
    private readonly IRepository<PatientProfile> _profilesRepository;
    public GetPatientProfileByIdQueryHandler(IRepository<PatientProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves <see cref="PatientProfile"/> by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<PatientProfile> Handle(GetPatientProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _profilesRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Patient Profile with such id does not exist");
        }

        return office;
    }
}
