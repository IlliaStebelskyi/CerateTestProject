using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;

public class Task2(ILogger<Task2> logger, RequestDelegate request)
{
    private readonly ILogger<Task2> _logger = logger;
    private readonly RequestDelegate _request = request;


    public async Task InvokeAsync(HttpContext context)
    {


        try
        {
            _logger.LogInformation("{Method}", context.Request.Method);
            await _request(context);
        }
        catch (Exception e)
        {
            var error = new Dictionary<string, string>
            {
                {"Type", e.GetType().ToString()},
                {"Message", e.Message},
                {"StackTrace", e.StackTrace}
            };
            string? json = JsonConvert.SerializeObject(error, Formatting.Indented);

            _logger.LogError("{error}", json);

            var clientError = new { error = "Internal server error" };
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(clientError));
        }
    }
}