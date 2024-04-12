using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using EasyClinic.ProfilesService.Domain.Contracts;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Queries;

/// <summary>
/// Query to Get <see cref="DoctorProfile"/> by Id.
/// </summary>
public record GetDoctorProfileByIdQuery : IRequest<DoctorProfile>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Handler for <see cref="GetDoctorProfileByIdQuery"/>
/// </summary>
public class GetDoctorProfileByIdQueryHandler : IRequestHandler<GetDoctorProfileByIdQuery, DoctorProfile>
{
    private readonly IRepository<DoctorProfile> _profilesRepository;
    public GetDoctorProfileByIdQueryHandler(IRepository<DoctorProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
    }

    /// <summary>
    /// Retrieves Doctor Profile by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<DoctorProfile> Handle(GetDoctorProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _profilesRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Doctor Profile with such id does not exist");
        }

        return office;
    }
}
