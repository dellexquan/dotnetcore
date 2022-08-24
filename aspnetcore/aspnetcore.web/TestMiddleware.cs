namespace aspnetcore.web;
public class TestMiddleware
{
    private readonly RequestDelegate next;

    public TestMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync("Test middleware start<br/>");
        await next.Invoke(context);
        await context.Response.WriteAsync("Test middleware end<br/>");
    }
}