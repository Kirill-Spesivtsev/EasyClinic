using MediatR;
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Exceptions;

namespace EasyClinic.SpecializationsSpecialization.Application.Queries;

/// <summary>
/// Query to Get <see cref="Specialization"/> by Id.
/// </summary>
public record GetSpecializationByIdQuery : IRequest<Specialization>
{
    public Guid Id { get; init; }
};

/// <summary>
/// Handler for <see cref="GetSpecializationByIdQuery"/>
/// </summary>
public class GetSpecializationByIdQueryHandler : IRequestHandler<GetSpecializationByIdQuery, Specialization>
{
    private readonly IRepository<Specialization> _specializationsRepository;
    public GetSpecializationByIdQueryHandler(IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
    }

    /// <summary>
    /// Retrieves <see cref="Specialization"/> by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Specialization> Handle(GetSpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _specializationsRepository.GetByIdAsync(request.Id);
        
        if (office == null)
        {
            throw new NotFoundException("Doctor Profile with such id does not exist");
        }

        return office;
    }
}
