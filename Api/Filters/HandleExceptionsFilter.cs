using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Api.Filters;

public class HandleExceptionsFilter : IExceptionFilter
{
    private readonly ILogger<HandleExceptionsFilter> _logger;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public HandleExceptionsFilter(ILogger<HandleExceptionsFilter> logger, ProblemDetailsFactory problemDetailsFactory)
    {
        _logger = logger;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var httpContext = context.HttpContext;

        ProblemDetails problemDetails = exception switch
        {
            SavingChangesFailedException => CreateProblemDetails(exception, httpContext, "Saving changes failed", 500),
            NotFoundException => CreateProblemDetails(exception, httpContext, "Not Found", 404),
            _ => throw exception
        };

        context.ExceptionHandled = true;
        context.Result = new ObjectResult(problemDetails);
    }

    private ProblemDetails CreateProblemDetails(Exception exception, HttpContext httpContext, string title, int statusCode)
    {
        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            httpContext,
            statusCode: statusCode,
            title: title,
            detail: exception.Message);

        // Log the exception
        _logger.LogError(exception, "An error occurred while processing the request.");

        return problemDetails;
    }
}
