using EasyClinic.ProfilesService.Application.Commands;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ProfilesService.Api.Controllers;

[ApiController]
[Route("api/v1/patient/profile")]
public class PatientProfileController : ControllerBase
{
    private readonly ILogger<PatientProfileController> _logger;
    private readonly IMediator _mediator;

    public PatientProfileController(IMediator mediator, ILogger<PatientProfileController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new PatientProfile and returns it.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created instance of PatientProfile</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePatientProfile(PatientProfileDto data,
        CancellationToken cancellationToken = default)
    {
        var request = new CreatePatientProfileCommand{ PatientProfileData = data };

        var createdPatient = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreatePatientProfile), new { id = createdPatient.Id }, createdPatient);
    }

    /// <summary>
    /// Edits an existing PatientProfile by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="profile"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditPatientProfile([FromRoute] Guid id, PatientProfileDto profileData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditPatientProfileCommand{Id = id, PatientProfileData = profileData};
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Deletes an existing PatientProfile by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePatientProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeletePatientProfileCommand{Id = id};
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves PatientProfile by id and returns it.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPatientProfile([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetPatientProfileByIdQuery{Id = id};
        var patient = await _mediator.Send(request, cancellationToken);

        return Ok(patient);
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
