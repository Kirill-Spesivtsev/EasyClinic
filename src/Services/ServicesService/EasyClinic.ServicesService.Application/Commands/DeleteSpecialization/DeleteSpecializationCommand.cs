using AutoMapper;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;
using MediatR;

namespace EasyClinic.SpecializationsSpecialization.Application.Commands;

/// <summary>
/// Command to delete Specialization.
/// </summary>
public class DeleteSpecializationCommand : IRequest
{
    public required Guid Id { get; set; }
}

/// <summary>
/// Command handler for <see cref="DeleteSpecializationCommand"/>
/// </summary>
public class DeleteSpecializationCommandHandler : IRequestHandler<DeleteSpecializationCommand>
{
    private readonly IRepository<Specialization> _specializationsRepository;

    public DeleteSpecializationCommandHandler(IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
    }

    /// <summary>
    /// Deletes Specialization by id.
    /// </summary>
    /// <returns></returns>
    public async Task Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
    {
        var Specialization = await _specializationsRepository.GetByIdAsync(request.Id);

        if (Specialization == null)
        {
            throw new NotFoundException($"Doctor Profile with id {request.Id} not found.");
        }

        await _specializationsRepository.DeleteAsync(Specialization);

    }

}
