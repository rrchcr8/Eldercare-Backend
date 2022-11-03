using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElderlyCare.API.Filters;

public class ErrorHandlingFilterAttrinute: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;
        context.Result = new ObjectResult(new { error = "An error occurred while processing your request" })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
        
    }
}
