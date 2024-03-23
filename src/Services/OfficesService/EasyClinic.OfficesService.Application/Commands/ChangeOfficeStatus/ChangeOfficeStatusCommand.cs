using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Enums;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus
{
    /// <summary>
    /// Commnd to change office status.
    /// </summary>
    public record ChangeOfficeStatusCommand : IRequest
    {
        /// <summary>
        /// Office Id to update.
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// New status for the office.
        /// </summary>
        public byte NewStatus { get; set; } = default!;
    }

    /// <summary>
    /// Handler for <see cref="ChangeOfficeStatusCommand"/>.
    /// </summary>
    public class ChangeOfficeStatusCommandHandler : IRequestHandler<ChangeOfficeStatusCommand>
    {
        private readonly IRepository<Office> _officesRepository;

        public ChangeOfficeStatusCommandHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }

        /// <summary>
        /// Updates office status by the given OfficeId.
        /// </summary>
        /// <remarks>
        /// <exception cref="NotFoundException">
        /// Thrown when office with given OfficeId does not exist.
        /// </exception>
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
