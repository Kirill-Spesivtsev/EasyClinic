using EasyClinic.ServicesService.Application.Commands;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ServicesService.Api.Controllers;

[ApiController]
[Route("api/v1/services")]
public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator, ILogger<ServiceController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new Service and returns it.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created instance of Service</returns>
    [HttpPost]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateService(ServiceDto data,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateServiceCommand{ ServiceData = data };

        var createdDoctor = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateService), new { id = createdDoctor.Id }, createdDoctor);
    }

    /// <summary>
    /// Edits an existing Service by id.
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
    public async Task<ActionResult> EditService([FromRoute] Guid id, ServiceDto profileData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditServiceCommand{Id = id, ServiceData = profileData};
        await _mediator.Send(request, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Deletes an existing Service by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteService([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteServiceCommand{Id = id};
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves an existing Service by id and returns it.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>One Service by id</returns>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetServiceById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetServiceByIdQuery{Id = id};
        var doctor = await _mediator.Send(request, cancellationToken);

        return Ok(doctor);
    }

    /// <summary>
    /// Retrieves all Services.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Services</returns>
    [HttpGet]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllServices(CancellationToken cancellationToken = default)
    {
        var request = new GetAllServicesQuery();
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }


}
