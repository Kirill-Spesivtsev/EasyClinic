using AutoMapper;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Enums;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.Commands
{
    /// <summary>
    /// Command to create new office.
    /// </summary>
    public record CreateOfficeCommand : IRequest<Office>
    {
        /// <summary>
        /// Office input data
        /// </summary>
        public required OfficeDto OfficeDto { get; set; } = default!;
    }

    /// <summary>
    /// Command handler for <see cref="CreateOfficeCommand"/>
    /// </summary>
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Office>
    {
        private readonly IRepository<Office> _officesRepository;
        private readonly IMapper _mapper;

        public CreateOfficeCommandHandler(IMapper mapper, 
            IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new office.
        /// </summary>
        /// <returns>Created <see cref="Office"/> instance</returns>
        public async Task<Office> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = _mapper.Map<OfficeDto, Office>(request.OfficeDto);

            await _officesRepository.AddAsync(office);

            return office;
        }
    }
}
