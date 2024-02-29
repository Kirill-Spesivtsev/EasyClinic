using Microsoft.AspNetCore.Mvc;

namespace EasyClinic.AuthService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        public void Get()
        {

        }
    }
}