using System.Collections.Concurrent;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Caching.Distributed;

public class Task3(ILogger<Task3> logger, RequestDelegate requestDelegate, ConcurrentDictionary<string, DateTime> requestLog, ICacheService cacheService1)
{
    private readonly ILogger<Task3> logger1 = logger;
    private readonly RequestDelegate requestDelegate1 = requestDelegate;
    private readonly ConcurrentDictionary<string, DateTime> _requestLog = requestLog;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var ipClient = httpContext.Connection.RemoteIpAddress.ToString() ?? "unknown";
        // var lastRequestTime = httpContext.Session.GetString("astRequestTime");
        if (_requestLog.TryGetValue(ipClient, out DateTime lastRequestTime))
        {
            if (DateTime.UtcNow - lastRequestTime < TimeSpan.FromSeconds(1))
            {
                httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await httpContext.Response.WriteAsync("Too many requests. Slow down.");
                return;
            }


        }

        _requestLog[ipClient] = DateTime.UtcNow;
        await requestDelegate1(httpContext);




        // RateLimitPartition.GetFixedWindowLimiter(
        //     partitionKey: ipClient,
        //     factory: partition => new FixedWindowRateLimiterOptions
        //     {
        //         AutoReplenishment = true,
        //         Window = TimeSpan.FromSeconds(1)
        //     }
        // )
        // )
    }
}