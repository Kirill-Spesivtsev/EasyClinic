using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Enums;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus
{
    public record ChangeOfficeStatusCommand : IRequest
    {
        public Guid OfficeId { get; set; }
        public byte NewStatus { get; set; } = default!;
    }

    public class ChangeOfficeStatusCommandHandler : IRequestHandler<ChangeOfficeStatusCommand>
    {
        private readonly IRepository<Office> _officesRepository;

        public ChangeOfficeStatusCommandHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }
        public async Task Handle(ChangeOfficeStatusCommand request, CancellationToken cancellationToken)
        {
            var office = await _officesRepository.GetByIdAsync(request.OfficeId);

            if (office == null)
            {
                throw new NotFoundException($"Office with id {request.OfficeId} does not exist");
            }

            office.Status = (OfficeStatus) request.NewStatus;

            await _officesRepository.UpdateAsync(office);
        }
    }
}
