using Microsoft.AspNetCore.Mvc;

namespace RPSLS.Game.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Hi");
            return Ok();
        }
    }
}
