using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnetcore.filter;

public class LogExceptionFilter : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        //System.Console.WriteLine("Log error!");
        await File.AppendAllTextAsync("error.log", context.Exception.ToString());
    }
}