using EasyClinic.AppointmentsService.Application.Commands;
using EasyClinic.AppointmentsService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyClinic.AppointmentsService.Api.Controllers;

[ApiController]
[Route("api/v1/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly ILogger<AppointmentsController> _logger;
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator, ILogger<AppointmentsController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentCommand request, 
        CancellationToken cancellation)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        request.PatientEmail = email;
        request.PatientUserName = username;

        await _mediator.Send(request, cancellation);

        return Ok();
    }

    [HttpPost("by-admin")]
    [Authorize]
    public async Task<IActionResult> CreateAppointmentByAdmin(CreateAppointmentCommand request, 
        CancellationToken cancellation)
    {
        await _mediator.Send(request, cancellation);

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsListForToday([FromQuery] GetAppointmentsListQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    [HttpGet("for-doctor")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsHistoryForDoctor([FromQuery] GetAppointmentsHistoryForDoctorQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    [HttpGet("for-patient")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsHistoryForPatient([FromQuery]GetAppointmentsHistoryForPatientQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    [HttpPost("result")]
    [Authorize(Roles = "Doctor, Receptionist, Admin")]
    public async Task<IActionResult> CreateAppointmentResult(CreateAppointmentResultCommand request, 
        CancellationToken cancellation)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        request.Email = email;
        request.UserName = username;

        await _mediator.Send(request, cancellation);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> CancelAppointment([FromRoute] Guid id,
        CancellationToken cancellation)
    {
        var request = new CancelAppointmentCommand{Id = id};
        await _mediator.Send(request, cancellation); 

        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Doctor, Receptionist, Admin")]
    public async Task<IActionResult> UpdateAppointmentResult(UpdateAppointmentResultCommand request, 
        CancellationToken cancellation)
    {
        await _mediator.Send(request, cancellation); 

        return Ok();
    }


}
