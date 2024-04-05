using AutoMapper;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.OfficesService.Application.Commands
{
    /// <summary>
    /// Command to update office by id.
    /// </summary>
    public record DeleteOfficeCommand : IRequest
    {
        [FromRoute]
        public required Guid Id { get; set; }
    }

    /// <summary>
    /// Handler for <see cref="DeleteOfficeCommand"/>
    /// </summary>
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand>
    {
        private readonly IRepository<Office> _officesRepository;
        public DeleteOfficeCommandHandler(IMapper mapper,
             IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }

        /// <summary>
        /// Updates office by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">
        /// Thrown when office with gven id does not exist.
        /// </exception>
        public async Task Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _officesRepository.GetByIdAsync(request.Id);
            if (office == null)
            {
                throw new NotFoundException("Office with such id does not exist");
            }

            await _officesRepository.DeleteAsync(office);
        }
    }
}
