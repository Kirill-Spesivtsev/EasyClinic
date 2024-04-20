using AutoMapper;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;
using System.Text.Json.Serialization;

namespace EasyClinic.SpecializationsSpecialization.Application.Commands;

/// <summary>
/// Command to update Specialization.
/// </summary>
public class EditSpecializationCommand : IRequest
{
    public required Guid Id { get; set; }
    public required SpecializationDto SpecializationData { get; set; }
}

/// <summary>
/// Command handler for <see cref="EditSpecializationCommand"/>
/// </summary>
public class EditSpecializationCommandHandler : IRequestHandler<EditSpecializationCommand>
{
    private readonly IRepository<Specialization> _specializationsRepository;
    private readonly IMapper _mapper;

    public EditSpecializationCommandHandler(IMapper mapper, 
        IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Updates Specialization by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(EditSpecializationCommand request, CancellationToken cancellationToken)
    {
        var Specialization = await _specializationsRepository.GetByIdAsync(request.Id);

        if (Specialization == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        _mapper.Map(request.SpecializationData, Specialization);

        await _specializationsRepository.UpdateAsync(Specialization);

    }

}

