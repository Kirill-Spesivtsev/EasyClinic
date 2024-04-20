using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Enums;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.SpecializationsSpecialization.Application.Commands;

/// <summary>
/// Command to change specialization status.
/// </summary>
public record ChangeSpecializationStatusCommand : IRequest
{
    public Guid SpecializationId { get; set; }

    public byte NewStatus { get; set; } = default!;
}

/// <summary>
/// Handler for <see cref="ChangeSpecializationStatusCommand"/>.
/// </summary>
public class ChangeSpecializationStatusCommandHandler : IRequestHandler<ChangeSpecializationStatusCommand>
{
    private readonly IRepository<Specialization> _specializationsRepository;

    public ChangeSpecializationStatusCommandHandler(IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
    }

    /// <summary>
    /// Updates specialization status by the given Specialization Id.
    /// </summary>
    public async Task Handle(ChangeSpecializationStatusCommand request, CancellationToken cancellationToken)
    {
        var specialization = await _specializationsRepository.GetByIdAsync(request.SpecializationId);

        if (specialization == null)
        {
            throw new NotFoundException($"Specialization with id {request.SpecializationId} does not exist");
        }

        specialization.Status = (Status) request.NewStatus;

        await _specializationsRepository.UpdateAsync(specialization);
    }
}


