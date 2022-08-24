using System.Text.Json;

namespace aspnetcore.web;
public class CheckAndParsingMiddleware
{
    private readonly RequestDelegate next;

    public CheckAndParsingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var pwd = context.Request.Query["password"];
        if (pwd == "123")
        {
            if (context.Request.HasJsonContentType())
            {
                var reqStream = context.Request.BodyReader.AsStream();
                //var jsonObj = await DJson.ParseAsync(reqStream);
                var jsonObj = JsonSerializer.Deserialize<dynamic?>(reqStream);
                context.Items["BodyJson"] = jsonObj;
            }
            await next.Invoke(context);
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }
}