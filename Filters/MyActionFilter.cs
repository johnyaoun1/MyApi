using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;  // For logging

public class MyActionFilter : IActionFilter
{
    private readonly ILogger<MyActionFilter> _logger;

    public MyActionFilter(ILogger<MyActionFilter> logger)
    {
        _logger = logger;
    }

    // This method is called before the action method is executed
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action is about to be executed: {ActionName}", context.ActionDescriptor.DisplayName);
    }

    // This method is called after the action method has been executed
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action has been executed: {ActionName}", context.ActionDescriptor.DisplayName);
    }
}
