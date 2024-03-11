using EasyClinic.OfficesService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
