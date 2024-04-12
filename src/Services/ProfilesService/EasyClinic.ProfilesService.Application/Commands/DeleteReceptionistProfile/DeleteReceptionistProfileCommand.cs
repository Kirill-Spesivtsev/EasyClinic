using AutoMapper;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to delete new ReceptionistProfile.
/// </summary>
public class DeleteReceptionistProfileCommand : IRequest
{
    public required Guid Id { get; set; }
}

/// <summary>
/// Command handler for <see cref="DeleteReceptionistProfileCommand"/>
/// </summary>
public class DeleteReceptionistProfileCommandHandler : IRequestHandler<DeleteReceptionistProfileCommand>
{
    private readonly IRepository<ReceptionistProfile> _profilesRepository;

    public DeleteReceptionistProfileCommandHandler(IRepository<ReceptionistProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Deletes ReceptionistProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(DeleteReceptionistProfileCommand request, CancellationToken cancellationToken)
    {
        var ReceptionistProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (ReceptionistProfile == null)
        {
            throw new NotFoundException($"Receptionist Profile with id {request.Id} not found.");
        }

        await _profilesRepository.DeleteAsync(ReceptionistProfile);

    }

}
