using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using TestApp.Web.Domain.Abstraction.Exceptions;

namespace TestApp.WebApi.Exception;
public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError; // Код по умолчанию
        
        if (context.Exception is UnauthorizedAccessException)
        {
            statusCode = (int)HttpStatusCode.Forbidden;
        }
        else if (context.Exception is NotFoundException)
        {
            statusCode = (int)HttpStatusCode.NotFound;
        }
        else if (context.Exception is ArgumentException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
        }
        
        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = "Произошла ошибка",
            Error = context.Exception.Message
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}