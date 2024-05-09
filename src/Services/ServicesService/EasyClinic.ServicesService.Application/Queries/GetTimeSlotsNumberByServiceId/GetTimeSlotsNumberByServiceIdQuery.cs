using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using EasyClinic.ServicesService.Domain.Contracts;
using MediatR;

namespace EasyClinic.ServicesService.Application.Queries;

/// <summary>
/// Query to Get Time slots size by Service Id.
/// </summary>
public record GetTimeSlotsNumberByServiceIdQuery : IRequest<int>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Handler for <see cref="GetTimeSlotsNumberByServiceIdQuery"/>
/// </summary>
public class GetTimeSlotsSizeByServiceIdQueryHandler : IRequestHandler<GetTimeSlotsNumberByServiceIdQuery, int>
{
    private readonly IServicesRepository _servicesRepository;
    public GetTimeSlotsSizeByServiceIdQueryHandler(IServicesRepository servicesRepository)
    {
        _servicesRepository = servicesRepository;
    }

    /// <summary>
    /// Retrieves Time slots size by Service Id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<int> Handle(GetTimeSlotsNumberByServiceIdQuery request, CancellationToken cancellationToken)
    {
        var service = await _servicesRepository.GetByIdAsync(request.Id);
        
        if (service == null)
        {
            throw new NotFoundException("Service with such id does not exist");
        }

        return service.Category.TimeSlotSize;
    }
}
