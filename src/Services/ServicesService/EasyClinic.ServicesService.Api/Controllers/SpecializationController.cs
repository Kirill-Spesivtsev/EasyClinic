using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.SpecializationsSpecialization.Application.Commands;
using EasyClinic.SpecializationsSpecialization.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.SpecializationsSpecialization.Api.Controllers;

[ApiController]
[Route("api/v1/specializations")]
public class SpecializationController : ControllerBase
{
    private readonly ILogger<SpecializationController> _logger;
    private readonly IMediator _mediator;

    public SpecializationController(IMediator mediator, ILogger<SpecializationController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new Specialization and returns it.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created instance of Specialization</returns>
    [HttpPost]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateSpecialization(SpecializationDto inputData,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateSpecializationCommand{ SpecializationData = inputData };

        var createdDoctor = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateSpecialization), 
            new { id = createdDoctor.Id }, createdDoctor);
    }

    /// <summary>
    /// Edits an existing Specialization by id.
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
    public async Task<ActionResult> EditSpecialization([FromRoute] Guid id, 
        SpecializationDto specializationData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditSpecializationCommand{Id = id, SpecializationData = specializationData};
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Deletes an existing Specialization by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSpecialization([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteSpecializationCommand{Id = id};
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Updates the status of an specialization by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update-status")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateSpecializationStatus(
        ChangeSpecializationStatusCommand request,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Retrieves all Specializations.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Specializations</returns>
    [HttpGet]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllSpecializations(CancellationToken cancellationToken = default)
    {
        var request = new GetAllSpecializationsQuery();
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }
}
