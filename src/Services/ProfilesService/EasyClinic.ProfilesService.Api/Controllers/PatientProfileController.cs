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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePatientProfile(PatientProfileDto data,
        CancellationToken cancellationToken = default)
    {
        var request = new CreatePatientProfileCommand{ PatientProfileData = data };

        var createdOffice = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreatePatientProfile), new { id = createdOffice.Id }, createdOffice);
    }

    /// <summary>
    /// Edits an existing PatientProfile by id.
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
    public async Task<ActionResult> EditPatientProfile([FromRoute] Guid id, PatientProfileDto profileData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditPatientProfileCommand{Id = id, PatientProfileData = profileData};
        await _mediator.Send(request, cancellationToken);

        return Ok(new { message = "Office was edited" });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
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
        [Authorize(Roles = "Receptionist, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPatientProfile([FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var request = new GetPatientProfileByIdQuery{Id = id};
            var office = await _mediator.Send(request, cancellationToken);

            return Ok(office);
        }

}
