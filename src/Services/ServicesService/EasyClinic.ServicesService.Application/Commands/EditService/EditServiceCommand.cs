using AutoMapper;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;
using System.Text.Json.Serialization;

namespace EasyClinic.ServicesService.Application.Commands;

/// <summary>
/// Command to update Service.
/// </summary>
public class EditServiceCommand : IRequest
{
    public required Guid Id { get; set; }
    public required ServiceDto ServiceData { get; set; }
}

/// <summary>
/// Command handler for <see cref="EditServiceCommand"/>
/// </summary>
public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand>
{
    private readonly IServicesRepository _profilesRepository;
    private readonly IMapper _mapper;

    public EditServiceCommandHandler(IMapper mapper, 
        IServicesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Updates Service by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(EditServiceCommand request, CancellationToken cancellationToken)
    {
        var Service = await _profilesRepository.GetByIdAsync(request.Id);

        if (Service == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        _mapper.Map(request.ServiceData, Service);

        await _profilesRepository.UpdateAsync(Service);

    }

}

