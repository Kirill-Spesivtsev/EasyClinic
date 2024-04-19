using AutoMapper;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to delete PatientProfile.
/// </summary>
public class DeletePatientProfileCommand : IRequest
{
    public required Guid Id { get; set; }
}

/// <summary>
/// Command handler for <see cref="DeletePatientProfileCommand"/>
/// </summary>
public class DeletePatientProfileCommandHandler : IRequestHandler<DeletePatientProfileCommand>
{
    private readonly IRepository<PatientProfile> _profilesRepository;

    public DeletePatientProfileCommandHandler(IRepository<PatientProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Deletes PatientProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(DeletePatientProfileCommand request, CancellationToken cancellationToken)
    {
        var patientProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (patientProfile == null)
        {
            throw new NotFoundException($"Patient Profile with id {request.Id} not found.");
        }

        await _profilesRepository.DeleteAsync(patientProfile);

    }

}
