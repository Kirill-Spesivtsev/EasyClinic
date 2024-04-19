using AutoMapper;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Domain.Exceptions;
using MediatR;
using System.Text.Json.Serialization;

namespace EasyClinic.ProfilesService.Application.Commands;

/// <summary>
/// Command to update ReceptionistProfile.
/// </summary>
public class EditReceptionistProfileCommand : IRequest
{
    public required Guid Id { get; set; }
    public required ReceptionistProfileDto ReceptionistProfileData { get; set; }
}

/// <summary>
/// Command handler for <see cref="EditReceptionistProfileCommand"/>
/// </summary>
public class EditReceptionistProfileCommandHandler : IRequestHandler<EditReceptionistProfileCommand>
{
    private readonly IRepository<ReceptionistProfile> _profilesRepository;
    private readonly IMapper _mapper;

    public EditReceptionistProfileCommandHandler(IMapper mapper, 
        IRepository<ReceptionistProfile> officesRepository)
    {
        _profilesRepository = officesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Updates ReceptionistProfile by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(EditReceptionistProfileCommand request, CancellationToken cancellationToken)
    {
        var ReceptionistProfile = await _profilesRepository.GetByIdAsync(request.Id);

        if (ReceptionistProfile == null)
        {
            throw new NotFoundException($"Receptionist Profile with id {request.Id} not found.");
        }

        _mapper.Map(request.ReceptionistProfileData, ReceptionistProfile);

        await _profilesRepository.UpdateAsync(ReceptionistProfile);

    }

}

