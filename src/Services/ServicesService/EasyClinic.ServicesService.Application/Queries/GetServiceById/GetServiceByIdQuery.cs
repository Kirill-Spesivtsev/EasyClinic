using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using EasyClinic.ServicesService.Domain.Contracts;
using MediatR;

namespace EasyClinic.ServicesService.Application.Queries;

/// <summary>
/// Query to Get <see cref="Service"/> by Id.
/// </summary>
public record GetServiceByIdQuery : IRequest<Service>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Handler for <see cref="GetServiceByIdQuery"/>
/// </summary>
public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Service>
{
    private readonly IRepository<Service> _profilesRepository;
    public GetServiceByIdQueryHandler(IRepository<Service> officesRepository)
    {
        _profilesRepository = officesRepository;
    }

    /// <summary>
    /// Retrieves <see cref="Service"/> by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _profilesRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Doctor Profile with such id does not exist");
        }

        return office;
    }
}
