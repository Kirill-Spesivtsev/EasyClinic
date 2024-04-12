using EasyClinic.ProfilesService.Application.Commands;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ProfilesService.Api.Controllers;

[ApiController]
[Route("api/v1/doctor/profile")]
public class DoctorProfileController : ControllerBase
{
    private readonly ILogger<DoctorProfileController> _logger;
    private readonly IMediator _mediator;

    public DoctorProfileController(IMediator mediator, ILogger<DoctorProfileController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new DoctorProfile and returns it.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created instance of DoctorProfile</returns>
    [HttpPost]
    [Authorize(Roles = "Doctor, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateDoctorProfile(DoctorProfileDto data,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateDoctorProfileCommand{ DoctorProfileData = data };

        var createdDoctor = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateDoctorProfile), new { id = createdDoctor.Id }, createdDoctor);
    }

    /// <summary>
    /// Edits an existing DoctorProfile by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="profile"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Doctor, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditDoctorProfile([FromRoute] Guid id, DoctorProfileDto profileData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditDoctorProfileCommand{Id = id, DoctorProfileData = profileData};
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Doctor, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDoctorProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteDoctorProfileCommand{Id = id};
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves DoctorProfile by id and returns it.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Doctor, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetDoctorProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetDoctorProfileByIdQuery{Id = id};
        var doctor = await _mediator.Send(request, cancellationToken);

        return Ok(doctor);
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
