using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Haseroz.WebApiTemplate.Web.HealthCheck.Checks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Haseroz.WebApiTemplate.Web.HealthCheck;

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
        services.AddHealthChecks()
            .AddCheck<KeycloakReadinessCheck>("keycloak_readiness");

        return services;
    }

    internal static IApplicationBuilder UseHealthChecksConfigs(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks("/api/health/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Name == "liveness",
                ResponseWriter = HealthCheckResponseWriter()
            })
            .UseHealthChecks("/api/health/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Name == "keycloak_readiness",
                ResponseWriter = HealthCheckResponseWriter(true)
            });
    }

    private static Func<HttpContext, HealthReport, Task> HealthCheckResponseWriter(bool includeDependencies = false)
    {
        return async (context, report) =>
        {
            var response = new HealthCheckResponse
            {
                Status = report.Status.ToString(),
                TotalDuration = report.TotalDuration.TotalMilliseconds,
                Dependencies = includeDependencies ? report.Entries.Select(entry => new HealthCheckDependency
                {
                    Name = entry.Key,
                    Status = entry.Value.Status.ToString(),
                    Duration = entry.Value.Duration.TotalMilliseconds,
                    Description = entry.Value.Description,
                    ErrorMessage = entry.Value.Exception?.Message
                }).ToList() : null
            };

            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, DefaultJsonOptions));
        };
    }
}

