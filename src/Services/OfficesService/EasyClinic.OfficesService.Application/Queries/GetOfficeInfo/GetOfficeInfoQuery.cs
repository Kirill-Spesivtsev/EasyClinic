using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;

namespace EasyClinic.OfficesService.Application.Queries
{
    /// <summary>
    /// Query to Get <see cref="Office"/> info by Id.
    /// </summary>
    public record GetOfficeInfoQuery : IRequest<Office>
    {
        public Guid Id { get; init; }
    };

    /// <summary>
    /// Handler for <see cref="GetOfficeInfoQuery"/>
    /// </summary>
    public class GetOfficeInfoQueryHandler : IRequestHandler<GetOfficeInfoQuery, Office>
    {
        private readonly IRepository<Office> _officesRepository;
        public GetOfficeInfoQueryHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }

        /// <summary>
        /// Retrieves office by its id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Office> Handle(GetOfficeInfoQuery request, CancellationToken cancellationToken)
        {
            var office = await _officesRepository.GetByIdAsync(request.Id);
            if (office == null)
            {
                throw new NotFoundException("Office with such id does not exist");
            }

            return office;
        }
    }
}
