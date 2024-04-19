using AutoMapper;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to create new DoctorProfile.
/// </summary>
public class CreateDoctorProfileCommand : IRequest<DoctorProfile>
{
    public required DoctorProfileDto DoctorProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="CreateDoctorProfileCommand"/>
/// </summary>
public class CreateDoctorProfileCommandHandler : IRequestHandler<CreateDoctorProfileCommand, DoctorProfile>
{
    private readonly IDoctorProfilesRepository _profilesRepository;
    private readonly IMapper _mapper;

    public CreateDoctorProfileCommandHandler(IMapper mapper, 
        IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates new DoctorProfile.
    /// </summary>
    /// <returns>Created <see cref="DoctorProfile"/> instance</returns>
    public async Task<DoctorProfile> Handle(CreateDoctorProfileCommand request, CancellationToken cancellationToken)
    {
        var office = _mapper.Map<DoctorProfileDto, DoctorProfile>(request.DoctorProfileData);

        await _profilesRepository.AddAsync(office);

        return office;
    }

}
