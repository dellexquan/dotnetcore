using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnetcore.filter;

public class MyExceptionFilter : IAsyncExceptionFilter
{
    private readonly IWebHostEnvironment hostEnv;

    public MyExceptionFilter(IWebHostEnvironment hostEnv)
    {
        this.hostEnv = hostEnv;
    }
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var msg = "";
        if (hostEnv.IsDevelopment())
        {
            msg = context.Exception.ToString();
        }
        else
        {
            msg = "No error!";
        }
        //System.Console.WriteLine("Error Code!");
        var objResult = new ObjectResult(new { code = 500, message = msg });
        context.Result = objResult;
        context.ExceptionHandled = true;
    }
}