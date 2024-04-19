using AutoMapper;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.ServicesService.Application.Commands;

/// <summary>
/// Command to delete Service.
/// </summary>
public class DeleteServiceCommand : IRequest
{
    public required Guid Id { get; set; }
}

/// <summary>
/// Command handler for <see cref="DeleteServiceCommand"/>
/// </summary>
public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
{
    private readonly IServicesRepository _profilesRepository;

    public DeleteServiceCommandHandler(IServicesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Deletes Service by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var Service = await _profilesRepository.GetByIdAsync(request.Id);

        if (Service == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        await _profilesRepository.DeleteAsync(Service);

    }

}
