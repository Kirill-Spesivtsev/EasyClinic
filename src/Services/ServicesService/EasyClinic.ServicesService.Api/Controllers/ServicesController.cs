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

}
