using AutoMapper;
using EasyClinic.ServicesService.Application.CQRS;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Application.Commands;

/// <summary>
/// Command to create new Service.
/// </summary>
public class CreateServiceCommand : IRequest<Service>
{
    public required ServiceDto ServiceData { get; set; }
}

/// <summary>
/// Command handler for <see cref="CreateServiceCommand"/>
/// </summary>
public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Service>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IMapper _mapper;

    public CreateServiceCommandHandler(IMapper mapper, 
        IServicesRepository servicesRepository)
    {
        _servicesRepository = servicesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates new Service.
    /// </summary>
    /// <returns>Created <see cref="Service"/> instance</returns>
    public async Task<Service> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = _mapper.Map<ServiceDto, Service>(request.ServiceData);

        await _servicesRepository.AddAsync(service);

        return service;
    }

}
