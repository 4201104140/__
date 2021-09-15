using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomConfig
{
    public class ThirdPartyActionFilter : IAsyncActionFilter
    {
        private ILogger _logger;

        public ThirdPartyActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ThreadStaticAttribute>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Third party action filter inward path.");

            await next().ConfigureAwait(false);

            _logger.LogInformation("Third party action filter outward path.");
        }
    }
}
