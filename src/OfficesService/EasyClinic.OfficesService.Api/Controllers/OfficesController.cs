using EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus;
using EasyClinic.OfficesService.Application.Commands.CreateOffice;
using EasyClinic.OfficesService.Application.Commands.EditOffice;
using EasyClinic.OfficesService.Application.Commands.UploadPhoto;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Application.Queries.GetAllOffices;
using EasyClinic.OfficesService.Application.Queries.GetOfficeInfo;
using EasyClinic.OfficesService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EasyClinic.OfficesService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/offices")]
    public class OfficesController : ControllerBase
    {
        private readonly ILogger<OfficesController> _logger;
        private readonly IMediator _mediator;

        public OfficesController(ILogger<OfficesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("list-offices")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllOffices(CancellationToken cancellationToken = default)
        {
            var request = new GetAllOfficesQuery();
            var offices = await _mediator.Send(request, cancellationToken);

            return Ok(offices);
        }

        [HttpPost("get-info")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOfficeInfo(GetOfficeInfoQuery request,
            CancellationToken cancellationToken = default)
        {
            var office = await _mediator.Send(request, cancellationToken);

            return Ok(office);
        }

        [HttpPost("update-status")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOfficeStatus(ChangeOfficeStatusCommand request,
            CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);

            return Ok(new { message = "Office status was updated" });
        }

        [HttpPost("create")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOffice(OfficeDto officeDto,
            CancellationToken cancellationToken = default)
        {
            var request = new CreateOfficeCommand{ OfficeDto = officeDto };
            var createdOffice = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(CreateOffice), new { id = createdOffice.Id }, createdOffice);
        }

        [HttpPost("edit"), DisableRequestSizeLimit]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditOffice(Guid id, OfficeDto office,
            CancellationToken cancellationToken = default)
        {
            var request = new EditOfficeCommand{ Id = id, OfficeDto = office };
            await _mediator.Send(request, cancellationToken);

            return Ok(new { message = "Office was edited" });
        }

        [HttpPost("upload-image"), DisableRequestSizeLimit]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UploadPhoto(IFormFile file,
            CancellationToken cancellationToken = default)
        {
            var request = new UploadPhotoCommand{ File = file };
            var imagePath = await _mediator.Send(request, cancellationToken);

            return Ok(imagePath);
        }
    }
}
