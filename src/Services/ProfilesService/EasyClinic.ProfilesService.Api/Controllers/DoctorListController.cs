using EasyClinic.OfficesService.Application.Queries;
using EasyClinic.ProfilesService.Application.Commands;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Application.Queries;
using EasyClinic.ProfilesService.Application.Queries.GetPatientProfilesByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.ProfilesService.Api.Controllers;

[ApiController]
[Route("api/v1/doctor-profiles")]
public class DoctorListController : ControllerBase
{
    private readonly ILogger<DoctorListController> _logger;
    private readonly IMediator _mediator;

    public DoctorListController(IMediator mediator, ILogger<DoctorListController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all doctors profiles.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of doctors</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllDoctors(CancellationToken cancellationToken = default)
    {
        var request = new GetAllDoctorProfilesQuery();
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }

    /// <summary>
    /// Retrieves doctors profiles filtered by office Id.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of doctors</returns>
    [HttpGet("office")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetDoctorsByOfficeId([FromRoute] Guid officeId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetDoctorProfilesByOffieIdQuery{ OfficeId = officeId };
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }

    /// <summary>
    /// Retrieves doctors profiles filtered by specialization.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of doctors</returns>
    [HttpGet("speciality")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetDoctorsBySpeciality([FromRoute] string specialization,
        CancellationToken cancellationToken = default)
    {
        var request = new GetDoctorProfilesBySpecialityQuery{ SpecializationName = specialization };
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }

    /// <summary>
    /// Retrieves Doctors profiles filtered by full name string pattern.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Doctors</returns>
    [HttpGet("name-search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetDoctorsByFullNamePattern([FromQuery] string fullNamePattern,
        CancellationToken cancellationToken = default)
    {
        var request = new FetchDoctorProfilesByFullNameStringQuery{ FullNameString = fullNamePattern };
        var doctors = await _mediator.Send(request, cancellationToken);

        return Ok(doctors);
    }

}
