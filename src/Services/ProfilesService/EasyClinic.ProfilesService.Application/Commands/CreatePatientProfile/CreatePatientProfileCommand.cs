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
/// Command to create new PatientProfile.
/// </summary>
public class CreatePatientProfileCommand : IRequest<PatientProfile>
{
    public required PatientProfileDto PatientProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="CreatePatientProfileCommand"/>
/// </summary>
public class CreatePatientProfileCommandHandler : IRequestHandler<CreatePatientProfileCommand, PatientProfile>
{
    private readonly IRepository<PatientProfile> _profilesRepository;
    private readonly IMapper _mapper;

    public CreatePatientProfileCommandHandler(IMapper mapper, 
        IRepository<PatientProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates new PatientProfile.
    /// </summary>
    /// <returns>Created <see cref="PatientProfile"/> instance</returns>
    public async Task<PatientProfile> Handle(CreatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        var office = _mapper.Map<PatientProfileDto, PatientProfile>(request.PatientProfileData);

        await _profilesRepository.AddAsync(office);

        return office;
    }

}
