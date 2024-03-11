using EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatusCommand;
using EasyClinic.OfficesService.Application.Queries.GetAllOffices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EasyClinic.OfficesService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/offices")]
    public class OfficesController : ControllerBase
    {
        private readonly ILogger<OfficesController> _logger;
        private readonly IMediator _mediator;

        public OfficesController(ILogger<OfficesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet("list-offices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllOffices(CancellationToken cancellationToken = default)
        {
            var request = new GetAllOfficesQuery();
            var offices = await _mediator.Send(request, cancellationToken);

            return Ok(offices);
        }

        [HttpPost("update-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOfficeStatus(ChangeOfficeStatusCommand request,
            CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);

            return Ok(new { message = "Office status updated successfully" });
        }
    }
}
