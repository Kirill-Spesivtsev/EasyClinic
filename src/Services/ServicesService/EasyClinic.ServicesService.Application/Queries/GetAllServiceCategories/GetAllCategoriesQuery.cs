
using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using MediatR;

namespace EasyClinic.CategorysCategory.Application.Queries;

/// <summary>
/// Query to Get all Service Categories.
/// </summary>
public record GetAllCategoriesQuery : IRequest<List<ServiceCategory>> { };

/// <summary>
/// Handler for <see cref="GetAllCategoriesQuery"/>
/// </summary>
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<ServiceCategory>>
{
    private readonly IRepository<ServiceCategory> _categorysRepository;

    public GetAllCategoriesQueryHandler(IRepository<ServiceCategory> categorysRepository)
    {
        _categorysRepository = categorysRepository;
    }

    /// <summary>
    /// Retrieves all Service Categories.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<ServiceCategory>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categorysRepository.GetAllAsync();
    }
}
