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
    private readonly IServicesRepository _servicesRepository;
    public GetServiceByIdQueryHandler(IServicesRepository servicesRepository)
    {
        _servicesRepository = servicesRepository;
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
        var office = await _servicesRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Doctor Profile with such id does not exist");
        }

        return office;
    }
}
