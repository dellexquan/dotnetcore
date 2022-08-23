using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace aspnetcore.filter;

public class RateLimitFilter : IAsyncActionFilter
{
    private readonly IMemoryCache memCache;
    public RateLimitFilter(IMemoryCache memCache)
    {
        this.memCache = memCache;

    }
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var remoteIP = context.HttpContext.Connection.RemoteIpAddress!.ToString();
        var key = $"LastVisitTick_{remoteIP}";
        var lastTick = memCache.Get<long?>(key);
        if (lastTick == null || Environment.TickCount64 - lastTick > 1000)
        {
            memCache.Set(key, Environment.TickCount64, TimeSpan.FromSeconds(10));
            return next();
        }
        else
        {
            context.Result = new ContentResult { StatusCode = 429 };
            return Task.CompletedTask;
        }
    }
}