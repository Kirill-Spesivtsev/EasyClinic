
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using MediatR;

namespace EasyClinic.ServicesService.Application.Queries;

/// <summary>
/// Query to Get all Services.
/// </summary>
public record GetAllServicesQuery : IRequest<List<Service>> { };

/// <summary>
/// Handler for <see cref="GetAllServicesQuery"/>
/// </summary>
public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, List<Service>>
{
    private readonly IServicesRepository _profilesRepository;

    public GetAllServicesQueryHandler(IServicesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves all Services.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository.GetAllAsync();
    }
}
