using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Shared.CustomResponse.Common;
using System.Diagnostics;

namespace Sample.API.Shared.CustomResponse;

public static class ExceptionResponse
{
    public static ObjectResult UnprocessableEntityDetails(HttpContext httpContext, ValidationException exc)
    {
        var statusCode = StatusCodes.Status422UnprocessableEntity;
        var problemDetails = new CustomDetails
        {
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = httpContext.Request.Path,
            Title = "Unprocessable Entity"
        };

        problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier);
        problemDetails.Extensions.Add("errors", exc.Errors.Select(e => new { Name = e.PropertyName, Message = e.ErrorMessage }));

        var result = new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };

        return result;
    }

    public static ObjectResult NotFoundDetails(HttpContext httpContext, System.Exception exc)
    {
        var statusCode = StatusCodes.Status404NotFound;
        var problemDetails = new CustomDetails
        {
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = httpContext.Request.Path,
            Title = "Not Found"
        };

        problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier);
        problemDetails.Extensions.Add("errors", exc.Message);

        var result = new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };

        return result;
    }

    public static ObjectResult ConflictDetails(HttpContext httpContext, System.Exception exc)
    {
        var statusCode = StatusCodes.Status409Conflict;
        var problemDetails = new CustomDetails
        {
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = httpContext.Request.Path,
            Title = "Conflict"
        };

        problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier);
        problemDetails.Extensions.Add("errors", exc.Message);

        var result = new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };

        return result;
    }
}