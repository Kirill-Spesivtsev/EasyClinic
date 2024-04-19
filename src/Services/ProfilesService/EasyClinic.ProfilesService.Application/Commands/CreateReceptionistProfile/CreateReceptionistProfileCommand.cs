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
/// Command to create new ReceptionistProfile.
/// </summary>
public class CreateReceptionistProfileCommand : IRequest<ReceptionistProfile>
{
    public required ReceptionistProfileDto ReceptionistProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="CreateReceptionistProfileCommand"/>
/// </summary>
public class CreateReceptionistProfileCommandHandler : IRequestHandler<CreateReceptionistProfileCommand, ReceptionistProfile>
{
    private readonly IRepository<ReceptionistProfile> _profilesRepository;
    private readonly IMapper _mapper;

    public CreateReceptionistProfileCommandHandler(IMapper mapper, 
        IRepository<ReceptionistProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates new ReceptionistProfile.
    /// </summary>
    /// <returns>Created <see cref="ReceptionistProfile"/> instance</returns>
    public async Task<ReceptionistProfile> Handle(CreateReceptionistProfileCommand request, CancellationToken cancellationToken)
    {
        var office = _mapper.Map<ReceptionistProfileDto, ReceptionistProfile>(request.ReceptionistProfileData);

        await _profilesRepository.AddAsync(office);

        return office;
    }

}
