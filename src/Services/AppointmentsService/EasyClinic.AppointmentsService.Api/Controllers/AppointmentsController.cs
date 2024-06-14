using EasyClinic.AppointmentsService.Application.Commands;
using EasyClinic.AppointmentsService.Application.Queries;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
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

    /// <summary>
    /// Creates new appointment.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Creates new appointment by admin.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPost("by-admin")]
    [Authorize]
    public async Task<IActionResult> CreateAppointmentByAdmin(CreateAppointmentCommand request, 
        CancellationToken cancellation)
    {
        await _mediator.Send(request, cancellation);

        return Ok();
    }

    /// <summary>
    /// Retrieves all appointments for today.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsListForToday([FromQuery] GetAppointmentsListQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    /// <summary>
    /// Retrieves appointments history for doctor.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("for-doctor")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsHistoryForDoctor([FromQuery] GetAppointmentsHistoryForDoctorQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    /// <summary>
    /// Retrieves appointments history for patient.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("for-patient")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetAppointmentsHistoryForPatient([FromQuery]GetAppointmentsHistoryForPatientQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    [HttpGet("timetable")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> GetAppointmentsSheduleForDoctorQuery([FromQuery] GetDoctorAppointmentsSheduleForADayQuery request, 
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

    /// <summary>
    /// Creates new appointment result.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Cancels appointment.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> CancelAppointment([FromRoute] Guid id,
        CancellationToken cancellation)
    {
        var request = new CancelAppointmentCommand{Id = id};
        await _mediator.Send(request, cancellation); 

        return Ok();
    }

    /// <summary>
    /// Updates appointment result by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "Doctor, Receptionist, Admin")]
    public async Task<IActionResult> UpdateAppointmentResult(UpdateAppointmentResultCommand request, 
        CancellationToken cancellation)
    {
        await _mediator.Send(request, cancellation); 

        return Ok();
    }

    /// <summary>
    /// Approves appointment by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPatch("approve/{id}")]
    public async Task<IActionResult> ApproveAppointment([FromRoute] Guid id, 
        CancellationToken cancellation)
    {
        var request = new ApproveAppointmentCommand{Id = id};
        await _mediator.Send(request, cancellation);

        return Ok();
    }

    /// <summary>
    /// Reshedules appointment by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpPatch("reshedule/{id}")]
    public async Task<IActionResult> ResheduleAppointment([FromRoute] Guid id, 
        CancellationToken cancellation)
    {
        var request = new ResheduleAppointmentCommand{Id = id};
        await _mediator.Send(request, cancellation);

        return Ok();
    }

    /// <summary>
    /// Retrieves day timesheet of service.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet("service-timesheet")]
    [Authorize(Roles = "Receptionist, Admin")]
    public async Task<IActionResult> GetSheduleOfServiceForADay([FromQuery] GetAllBookedTimeSlotsQuery request,
        CancellationToken cancellation)
    {
        var appointments = await _mediator.Send(request, cancellation);

        return Ok(appointments);
    }

}
