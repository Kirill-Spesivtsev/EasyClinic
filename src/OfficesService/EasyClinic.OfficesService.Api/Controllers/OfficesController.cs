using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.OfficesService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfficesController : ControllerBase
    {
        private readonly ILogger<OfficesController> _logger;

        public OfficesController(ILogger<OfficesController> logger)
        {
            _logger = logger;
        }

    }
}
