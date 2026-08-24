using System.Diagnostics;

public class Loging(ILogger<Loging> logger, RequestDelegate next)
{
    private readonly ILogger<Loging> _logger = logger;
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Request: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);

        await _next(httpContext);

        stopwatch.Stop();
        _logger.LogInformation("Completed in {Elapsed}ms", stopwatch.ElapsedMilliseconds);
    }
}