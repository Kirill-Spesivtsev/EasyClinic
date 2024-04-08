using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries
{
    /// <summary>
    /// Query to Get all offices.
    /// </summary>
    public record GetAllOfficesQuery : IRequest<List<Office>> { };

    /// <summary>
    /// Handler for <see cref="GetAllOfficesQuery"/>
    /// </summary>
    public class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, List<Office>>
    {
        private readonly IRepository<Office> _officesRepository;

        public GetAllOfficesQueryHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }

        /// <summary>
        /// Retrieves all offices
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Office>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            return await _officesRepository.GetAllAsync();
        }
    }
}
