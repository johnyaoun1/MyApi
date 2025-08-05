using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApi.Filters
{
    public class CallerValidationFilter : IActionFilter
    {
        private readonly ILogger<CallerValidationFilter> _logger;

        public CallerValidationFilter(ILogger<CallerValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var caller = context.HttpContext.Request.Headers["caller"].ToString();
            if (caller == "Unknown")
            {
                _logger.LogWarning("Request cancelled due to invalid caller.");
                context.Result = new BadRequestObjectResult("Request cancelled due to invalid caller.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
