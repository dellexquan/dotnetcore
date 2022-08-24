using aspnetcore.web;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.Map("/test", appbuilder =>
{
    appbuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 Start<br/>");
        await next.Invoke();
        await context.Response.WriteAsync("1 End<br/>");
    });
    appbuilder.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2 Start<br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2 End<br/>");
    });
    appbuilder.UseMiddleware<TestMiddleware>();
    appbuilder.Run(async ctx =>
    {
        await ctx.Response.WriteAsync("Hello middleware <br/>");
    });
});

app.Run();
