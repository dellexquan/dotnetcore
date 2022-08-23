using System.Transactions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnetcore.filter;

public class TransactionScopeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasNotTranscationAttribute = false;
        if (context.ActionDescriptor is ControllerActionDescriptor)
        {
            var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
            hasNotTranscationAttribute = !actionDesc.MethodInfo.IsDefined(typeof(TransactionAttribute), false);
        }
        if (hasNotTranscationAttribute)
        {
            await next();
            return;
        }
        using (var txScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var result = await next();
            if (result.Exception == null)
            {
                txScope.Complete();
            }
        }
    }
}