using AutoMapper;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyClinic.OfficesService.Application.Commands.EditOffice
{
    public record EditOfficeCommand : IRequest
    {
        public required int Id { get; set; }
        public required OfficeDto OfficeDto { get; set; } = default!;
    }

    public class UpdateOfficeCommandHandler : IRequestHandler<EditOfficeCommand>
    {
        private readonly IRepository<Office> _officesRepository;
        private readonly IMapper _mapper;
        public UpdateOfficeCommandHandler(IMapper mapper,
             IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
            _mapper = mapper;
        }

        public async Task Handle(EditOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _officesRepository.GetByIdAsync(request.Id);
            if (office == null)
            {
                throw new NotFoundException("Office with such id does not exist");
            }

            _mapper.Map(request.OfficeDto, office);

            await _officesRepository.UpdateAsync(office);
        }
    }
}
