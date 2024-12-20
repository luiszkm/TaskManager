using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Exceptions;
using TaskManager.Infra.EFCore.Exceptions;

namespace TaskManager.API.FilterException;

public class ApiGlobalExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _env;

    public ApiGlobalExceptionFilter(IHostEnvironment env)
    {
        _env = env;
    }
    public void OnException(ExceptionContext context)
    {
        var details = new ProblemDetails();
        var exception = context.Exception;

        if (_env.IsDevelopment())
        {
            details.Extensions.Add("StackTrace", exception.StackTrace);
        }

        switch (exception)
        {
            case NotFoundException:
                details.Title = "entity NotFound";
                details.Status = StatusCodes.Status404NotFound;
                details.Type = "Not Found";
                details.Detail = exception!.Message;
                break;
            case EntityValidationException:
                details.Title = "One or more validation errors occurred.";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnProcessableEntity";
                details.Detail = exception!.Message;
                break;

            case ApplicationUnauthorizedException:
                details.Title = "Invalid credential.";
                details.Status = StatusCodes.Status401Unauthorized;
                details.Type = "Unauthorized";
                details.Detail = exception!.Message;
                break;

            case NotFoundDBException:
                details.Title = "entity NotFound In DB";
                details.Status = StatusCodes.Status404NotFound;
                details.Type = "Not Found";
                details.Detail = exception!.Message;
                break;

            case BadRequestDbException:
                details.Title = "Bad Request In Database";
                details.Status = StatusCodes.Status400BadRequest;
                details.Type = "BadRequest";
                details.Detail = exception!.Message;
                break;


            default:
                details.Title = "An unexpected error occurred";
                details.Status = StatusCodes.Status500InternalServerError;
                details.Type = "UnexpectedError";
                details.Detail = exception.Message;
                break;
        }

        context.HttpContext.Response.StatusCode = (int)details.Status;
        context.Result = new ObjectResult(details);

    }
}
