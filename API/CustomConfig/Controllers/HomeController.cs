using CustomConfig.CustomProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomConfig.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly WidgetOptions _options;
        private readonly IFeatureManager _featureManager;

        public HomeController(IOptionsMonitor<WidgetOptions> options
            , IFeatureManager featureManager)
        {
            _options = options.CurrentValue;
            _featureManager = featureManager;
        }

        [HttpGet]
        [FeatureGate(MyFeatureFlags.Home)]
        public IActionResult Index()
        {
            return Ok(new { name = "Tai"});
        }

        [HttpGet("about")]
        public async Task<IActionResult> About()
        {
            if (await _featureManager.IsEnabledAsync(nameof(MyFeatureFlags.CustomViewData)))
            {
                return Ok(new { name = "Tai" });
            }
            return new BadRequestObjectResult(new { error= "Browser is not supported."});
        }

        [HttpGet("beta")]
        [FeatureGate(MyFeatureFlags.Beta)]
        public IActionResult Beta()
        {
            return Ok(new { name = "Tai" });
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return new BadRequestObjectResult(new { Error = "lol" });
        }
    }
}
