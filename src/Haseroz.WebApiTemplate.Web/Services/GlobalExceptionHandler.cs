using Haseroz.WebApiTemplate.Web.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Haseroz.WebApiTemplate.Web.Services;

internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error ocurred while processing the request: '{exceptionMessage}'", exception.Message);

        httpContext.Response.ContentType = "application/json";
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
