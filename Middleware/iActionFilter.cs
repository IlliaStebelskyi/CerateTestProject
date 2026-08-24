using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

public class LoggingFilter(ILogger<LoggingFilter> logger, string name) : Attribute, IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger = logger;
    private readonly string _name = name;
    private Stopwatch? _stopwatch;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
        foreach (var info in context.ActionArguments)
        {
            _logger.LogInformation("Action argument: {Key} = {Value}", info.Key, info.Value);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch?.Stop();
        var statusCode = context.HttpContext.Response.StatusCode;

        _logger.LogInformation("Status: {StatusCode}, Elapsed: {Elapsed}ms", statusCode, _stopwatch?.ElapsedMilliseconds);
    }
}