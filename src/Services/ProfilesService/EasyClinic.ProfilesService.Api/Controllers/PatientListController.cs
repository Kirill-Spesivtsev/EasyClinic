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
[Route("api/v1/patient-profiles")]
public class PatientListController : ControllerBase
{
    private readonly ILogger<PatientListController> _logger;
    private readonly IMediator _mediator;

    public PatientListController(IMediator mediator, ILogger<PatientListController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all Patients profiles.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Patients</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPatients(CancellationToken cancellationToken = default)
    {
        var request = new GetAllPatientProfilesQuery();
        var patients = await _mediator.Send(request, cancellationToken);

        return Ok(patients);
    }
    
    /// <summary>
    /// Retrieves Patients profiles filtered by full name string pattern.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Patients</returns>
    [HttpGet("name-search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPatientsByFullNamePattern([FromQuery] string fullNamePattern,
        CancellationToken cancellationToken = default)
    {
        var request = new FetchPatientProfilesByFullNameStringQuery{ FullNameString = fullNamePattern };
        var patients = await _mediator.Send(request, cancellationToken);

        return Ok(patients);
    }

}
