using AutoMapper;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;
using System.Text.Json.Serialization;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to update new DoctorProfile.
/// </summary>
public class EditDoctorProfileCommand : IRequest
{
    public required Guid Id { get; set; }
    public required DoctorProfileDto DoctorProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="EditDoctorProfileCommand"/>
/// </summary>
public class EditDoctorProfileCommandHandler : IRequestHandler<EditDoctorProfileCommand>
{
    private readonly IDoctorProfilesRepository _profilesRepository;
    private readonly IMapper _mapper;

    public EditDoctorProfileCommandHandler(IMapper mapper, 
        IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Updates DoctorProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(EditDoctorProfileCommand request, CancellationToken cancellationToken)
    {
        var DoctorProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (DoctorProfile == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        _mapper.Map(request.DoctorProfileData, DoctorProfile);

        await _profilesRepository.UpdateAsync(DoctorProfile);

    }

}

