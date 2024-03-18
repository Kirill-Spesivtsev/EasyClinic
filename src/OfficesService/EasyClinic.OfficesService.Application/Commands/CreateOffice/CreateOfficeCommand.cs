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

namespace EasyClinic.OfficesService.Application.Commands.CreateOffice
{
    public record CreateOfficeCommand : IRequest
    {
        public required OfficeDto OfficeDto { get; set; } = default!;
    }

    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand>
    {
        private readonly IRepository<Office> _officesRepository;
        private readonly IMapper _mapper;

        public CreateOfficeCommandHandler(IMapper mapper, 
            IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = _mapper.Map<OfficeDto, Office>(request.OfficeDto);

            await _officesRepository.AddAsync(office);
        }
    }
}
