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

}
