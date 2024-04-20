using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
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
/// Command to change service status.
/// </summary>
public record ChangeServiceStatusCommand : IRequest
{
    public Guid ServiceId { get; set; }

    public byte NewStatus { get; set; } = default!;
}

/// <summary>
/// Handler for <see cref="ChangeServiceStatusCommand"/>.
/// </summary>
public class ChangeServiceStatusCommandHandler : IRequestHandler<ChangeServiceStatusCommand>
{
    private readonly IRepository<Specialization> _servicesRepository;

    public ChangeServiceStatusCommandHandler(IRepository<Specialization> servicesRepository)
    {
        _servicesRepository = servicesRepository;
    }

    /// <summary>
    /// Updates service status by the given Service Id.
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


