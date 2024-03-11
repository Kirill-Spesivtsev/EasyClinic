using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.Queries.GetAllOffices
{
    public record GetAllOfficesQuery : IRequest<List<Office>> { };

    public class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, List<Office>>
    {
        private readonly IRepository<Office> _officesRepository;
        public GetAllOfficesQueryHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }
        public async Task<List<Office>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            return await _officesRepository.GetAllAsync();
        }
    }
}
