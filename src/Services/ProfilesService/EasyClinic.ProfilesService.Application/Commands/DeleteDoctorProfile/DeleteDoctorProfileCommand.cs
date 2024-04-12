using AutoMapper;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to delete new DoctorProfile.
/// </summary>
public class DeleteDoctorProfileCommand : IRequest
{
    public required Guid Id { get; set; }
}

/// <summary>
/// Command handler for <see cref="DeleteDoctorProfileCommand"/>
/// </summary>
public class DeleteDoctorProfileCommandHandler : IRequestHandler<DeleteDoctorProfileCommand>
{
    private readonly IDoctorProfilesRepository _profilesRepository;

    public DeleteDoctorProfileCommandHandler(IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Deletes DoctorProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(DeleteDoctorProfileCommand request, CancellationToken cancellationToken)
    {
        var DoctorProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (DoctorProfile == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        await _profilesRepository.DeleteAsync(DoctorProfile);

    }

}
