using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : Attribute, IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger = logger;
    public void OnException(ExceptionContext context)
    {
        var error = new Dictionary<string, string>
    {
        {"Type", context.Exception.GetType().ToString()},
        {"Message", context.Exception.Message},
        {"StackTrace", context.Exception.StackTrace}
    };

        _logger.LogError("{Error}", JsonConvert.SerializeObject(error, Formatting.Indented));


        context.Result = new JsonResult(new { error = "Internal server error" })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;

    }
}
