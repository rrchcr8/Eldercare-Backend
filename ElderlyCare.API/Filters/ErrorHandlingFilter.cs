using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ElderlyCare.API.Filters;

public class ErrorHandlingFilterAttrinute: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;
        var problemDetails = new ProblemDetails() 
        { 
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occurred while processing your request perro",
            Instance = context.HttpContext.Request.Path,
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = exception.Message
        };
        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}
