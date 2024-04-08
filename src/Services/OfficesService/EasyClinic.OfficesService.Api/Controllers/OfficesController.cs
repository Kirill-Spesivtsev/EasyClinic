using EasyClinic.OfficesService.Application.Commands;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Application.Queries;
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

        public OfficesController(IMediator mediator, ILogger<OfficesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all offices using a returns it.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of offices</returns>
        [HttpGet]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllOffices(CancellationToken cancellationToken = default)
        {
            var request = new GetAllOfficesQuery();
            var offices = await _mediator.Send(request, cancellationToken);

            return Ok(offices);
        }

        /// <summary>
        /// Retrieves office information based on the providedid and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Office information</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOfficeInfo([FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var request = new GetOfficeInfoQuery{Id = id};
            var office = await _mediator.Send(request, cancellationToken);

            return Ok(office);
        }

        /// <summary>
        /// Updates the status of an office by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new office and returns it.
        /// </summary>
        /// <param name="officeData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Created office</returns>
        [HttpPost]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOffice(OfficeDto officeData,
            CancellationToken cancellationToken = default)
        {
            var request = new CreateOfficeCommand{ OfficeDto = officeData };
            var createdOffice = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(CreateOffice), new { id = createdOffice.Id }, createdOffice);
        }

        /// <summary>
        /// Edits an existing office by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="office"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}"), DisableRequestSizeLimit]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditOffice([FromRoute] Guid id, OfficeDto office,
            CancellationToken cancellationToken = default)
        {
            var request = new EditOfficeCommand{ Id = id, OfficeDto = office };
            await _mediator.Send(request, cancellationToken);

            return Ok(new { message = "Office was edited" });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOffice([FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteOfficeCommand{Id = id};
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Uploads an image of an office to the image store and returns its path.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image path string</returns>
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
