
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using MediatR;

namespace EasyClinic.SpecializationsSpecialization.Application.Queries;

/// <summary>
/// Query to Get all Specializations.
/// </summary>
public record GetAllSpecializationsQuery : IRequest<List<Specialization>> { };

/// <summary>
/// Handler for <see cref="GetAllSpecializationsQuery"/>
/// </summary>
public class GetAllSpecializationsQueryHandler : IRequestHandler<GetAllSpecializationsQuery, List<Specialization>>
{
    private readonly IRepository<Specialization> _specializationsRepository;

    public GetAllSpecializationsQueryHandler(IRepository<Specialization> specializationsRepository)
    {
        _specializationsRepository = specializationsRepository;
    }

    /// <summary>
    /// Retrieves all Specializations.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Specialization>> Handle(GetAllSpecializationsQuery request, CancellationToken cancellationToken)
    {
        return await _specializationsRepository.GetAllAsync();
    }
}
