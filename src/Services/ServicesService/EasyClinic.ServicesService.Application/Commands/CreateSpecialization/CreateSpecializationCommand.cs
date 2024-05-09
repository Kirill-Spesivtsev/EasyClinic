using AutoMapper;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using MediatR;

namespace EasyClinic.SpecializationsSpecialization.Application.Commands;

/// <summary>
/// Command to create new Specialization.
/// </summary>
public class CreateSpecializationCommand : IRequest<Specialization>
{
    public required SpecializationDto SpecializationData { get; set; }
}

/// <summary>
/// Command handler for <see cref="CreateSpecializationCommand"/>
/// </summary>
public class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, Specialization>
{
    private readonly IRepository<Specialization> _specializationsRepository;
    private readonly IMapper _mapper;

    public CreateSpecializationCommandHandler(IMapper mapper, 
        IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates new Specialization.
    /// </summary>
    /// <returns>Created <see cref="Specialization"/> instance</returns>
    public async Task<Specialization> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = _mapper.Map<SpecializationDto, Specialization>(request.SpecializationData);

        await _specializationsRepository.AddAsync(specialization);

        return specialization;
    }

}
