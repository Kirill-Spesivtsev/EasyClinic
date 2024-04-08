using AutoMapper;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;
using System.Text.Json.Serialization;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to update new PatientProfile.
/// </summary>
public class EditPatientProfileCommand : IRequest
{
    public required Guid Id { get; set; }
    public required PatientProfileDto PatientProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="EditPatientProfileCommand"/>
/// </summary>
public class EditPatientProfileCommandHandler : IRequestHandler<EditPatientProfileCommand>
{
    private readonly IRepository<PatientProfile> _profilesRepository;
    private readonly IMapper _mapper;

    public EditPatientProfileCommandHandler(IMapper mapper, 
        IRepository<PatientProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Updates PatientProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(EditPatientProfileCommand request, CancellationToken cancellationToken)
    {
        var patientProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (patientProfile == null)
        {
            throw new NotFoundException($"Patient Profile with id {request.Id} not found.");
        }

        _mapper.Map(request.PatientProfileData, patientProfile);

        await _profilesRepository.UpdateAsync(patientProfile);

    }

}

