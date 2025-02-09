using System.Text.Json;
using System.Text.Json.Serialization;
using Haseroz.WebApiTemplate.Web.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Haseroz.WebApiTemplate.Web.Extensions;

internal static class HealthCheckExtensions
{
    private static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    internal static IServiceCollection AddHealthChecksConfigs(this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

    internal static IApplicationBuilder UseHealthChecksConfigs(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks("/api/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter()
            });
    }

    private static Func<HttpContext, HealthReport, Task> HealthCheckResponseWriter()
    {
        return async (context, report) =>
        {
            var response = new HealthCheckResponse
            {
                Status = report.Status.ToString(),
                TotalDuration = report.TotalDuration.TotalMilliseconds
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, DefaultJsonOptions));
        };
    }
}

