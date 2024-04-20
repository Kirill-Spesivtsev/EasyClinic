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




}
