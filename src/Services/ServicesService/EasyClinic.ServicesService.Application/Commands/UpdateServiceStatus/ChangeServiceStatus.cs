using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Enums;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Application.Commands;

/// <summary>
/// Commnd to change service status.
/// </summary>
public record ChangeServiceStatusCommand : IRequest
{
    /// <summary>
    /// Service Id to update.
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// New status for the service.
    /// </summary>
    public byte NewStatus { get; set; } = default!;
}

/// <summary>
/// Handler for <see cref="ChangeServiceStatusCommand"/>.
/// </summary>
public class ChangeServiceStatusCommandHandler : IRequestHandler<ChangeServiceStatusCommand>
{
    private readonly IServicesRepository _servicesRepository;

    public ChangeServiceStatusCommandHandler(IServicesRepository servicesRepository)
    {
        _servicesRepository = servicesRepository;
    }

    /// <summary>
    /// Updates service status by the given ServiceId.
    /// </summary>
    public async Task Handle(ChangeServiceStatusCommand request, CancellationToken cancellationToken)
    {
        var service = await _servicesRepository.GetByIdAsync(request.ServiceId);

        if (service == null)
        {
            throw new NotFoundException($"Service with id {request.ServiceId} does not exist");
        }

        service.Status = (Status) request.NewStatus;

        await _servicesRepository.UpdateAsync(service);
    }
}


