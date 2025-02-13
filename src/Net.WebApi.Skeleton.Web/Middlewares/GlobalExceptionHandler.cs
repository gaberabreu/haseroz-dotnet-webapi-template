using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Net.WebApi.Skeleton.Web.Models.Errors;

namespace Net.WebApi.Skeleton.Web.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error ocurred while processing the request: '{exceptionMessage}'", exception.Message);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = ErrorResponse.FromContext(httpContext).WithError(new()
        {
            Type = "InternalServerError",
            Error = "Internal Server Error",
            Detail = "An unexpected error ocurred. Please, try again later."
        });

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
