using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using EasyClinic.ProfilesService.Domain.Contracts;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Queries;

/// <summary>
/// Query to Get <see cref="ReceptionistProfile"/> by Id.
/// </summary>
public record GetReceptionistProfileByIdQuery : IRequest<ReceptionistProfile>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Query Handler to Get <see cref="ReceptionistProfile"/> by Id.
/// </summary>
public class GetReceptionistProfileByIdQueryHandler : IRequestHandler<GetReceptionistProfileByIdQuery, ReceptionistProfile>
{
    private readonly IRepository<ReceptionistProfile> _profilesRepository;
    public GetReceptionistProfileByIdQueryHandler(IRepository<ReceptionistProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
    }

    /// <summary>
    /// Retrieves Receptionist Profile by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<ReceptionistProfile> Handle(GetReceptionistProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _profilesRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Receptionist Profile with such id does not exist");
        }

        return office;
    }
}
