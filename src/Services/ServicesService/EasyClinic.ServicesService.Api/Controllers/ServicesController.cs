using EasyClinic.CategorysCategory.Application.Queries;
using EasyClinic.ServicesService.Application.Commands;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Application.Queries;
using EasyClinic.SpecializationsSpecialization.Application.Queries;
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
    public async Task<ActionResult> CreateService(ServiceDto inputData,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateServiceCommand{ ServiceData = inputData };

        var createdService = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateService), new { id = createdService.Id }, createdService);
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
    public async Task<ActionResult> EditService([FromRoute] Guid id, ServiceDto inputData,
        CancellationToken cancellationToken = default)
    {
        var request = new EditServiceCommand{Id = id, ServiceData = inputData};
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
    /// Updates the status of a Service by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update-status")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateServiceStatus(ChangeServiceStatusCommand request,
        CancellationToken cancellationToken = default)
    {
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
        var service = await _mediator.Send(request, cancellationToken);

        return Ok(service);
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
        var services = await _mediator.Send(request, cancellationToken);

        return Ok(services);
    }

    /// <summary>
    /// Retrieves all service Categories.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of service Categories</returns>
    [HttpGet("categories")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllServiceCategories(CancellationToken cancellationToken = default)
    {
        var request = new GetAllCategoriesQuery();
        var serviceCategories = await _mediator.Send(request, cancellationToken);

        return Ok(serviceCategories);
    }

    /// <summary>
    /// Retrieves SerivceCategory slot size by Service id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Slot Size value</returns>
    [HttpGet("get-slot-size/{id:guid}")]
    [Authorize(Roles = "Receptionist, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSlotsSizeByServiceId([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetTimeSlotsNumberByServiceIdQuery{Id = id};
        var slotSize = await _mediator.Send(request, cancellationToken);

        return Ok(slotSize);
    }
}
