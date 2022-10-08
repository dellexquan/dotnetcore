using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ddd.usermanager.api;

public class UnitOfWorkFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();

        if (result.Exception != null) return;

        var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;

        if (actionDesc == null) return;

        var uowAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();

        if (uowAttr == null) return;

        foreach (var dbContextType in uowAttr.DbContextTypes)
        {
            var dbContext = context.HttpContext.RequestServices.GetService(dbContextType) as DbContext;
            // System.Console.WriteLine("Get db context.");
            if (dbContext == null) return;
            await dbContext.SaveChangesAsync();
            System.Console.WriteLine("Db save changes.");
        }
    }
}