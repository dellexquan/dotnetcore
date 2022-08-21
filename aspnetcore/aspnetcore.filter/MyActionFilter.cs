using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnetcore.filter;

public class MyActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        System.Console.WriteLine("MyActionFilter: Pre-Action");
        var result = await next();
        if (result.Exception != null)
        {
            System.Console.WriteLine("MyActionFilter: Exception");
        }
        else
        {
            System.Console.WriteLine("MyActionFilter: Run success!");
        }

    }
}