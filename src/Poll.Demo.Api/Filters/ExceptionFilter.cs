using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Poll.Demo.Api.Models;

namespace Poll.Demo.Api.Filters;

public class ExceptionFilter : IExceptionFilter   
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;
        _logger.LogError(context.Exception, "An exception occured during action {action} execution", context.ActionDescriptor.DisplayName);
        context.Result = new ObjectResult(new WebApiActionResult
            { ErrorMessage = "Failed to execute request", IsSuccesfull = false }) { StatusCode = 500 };  
        context.ExceptionHandled = true;
    }
}