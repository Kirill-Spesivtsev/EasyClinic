using EasyClinic.ProfilesService.Application.Commands;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ProfilesService.Api.Controllers;

[ApiController]
[Route("api/v1/receptionist/profile")]
public class ReceptionistProfileController : ControllerBase
{
    private readonly ILogger<ReceptionistProfileController> _logger;
    private readonly IMediator _mediator;

    public ReceptionistProfileController(IMediator mediator, ILogger<ReceptionistProfileController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new ReceptionistProfile and returns it.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created instance of ReceptionistProfile</returns>
    [HttpPost]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateReceptionistProfile(ReceptionistProfileDto data,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateReceptionistProfileCommand{ ReceptionistProfileData = data };

        var createdReceptionist = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateReceptionistProfile), new { id = createdReceptionist.Id }, createdReceptionist);
    }

    /// <summary>
    /// Edits an existing ReceptionistProfile by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="profile"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditReceptionistProfile([FromRoute] Guid id, ReceptionistProfileDto profileData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditReceptionistProfileCommand{Id = id, ReceptionistProfileData = profileData};
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteReceptionistProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteReceptionistProfileCommand{Id = id};
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves ReceptionistProfile by id and returns it.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetReceptionistProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetReceptionistProfileByIdQuery{Id = id};
        var office = await _mediator.Send(request, cancellationToken);

        return Ok(office);
    }

    /// <summary>
    /// Uploads an image of person to the image store and returns its path.
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
