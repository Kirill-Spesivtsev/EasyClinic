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

public class GetPatientProfileByIdQueryHandler : IRequestHandler<GetPatientProfileByIdQuery, PatientProfile>
{
    private readonly IRepository<PatientProfile> _profilesRepository;
    public GetPatientProfileByIdQueryHandler(IRepository<PatientProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
    }
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
