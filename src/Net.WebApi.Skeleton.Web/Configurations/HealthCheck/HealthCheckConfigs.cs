using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net.WebApi.Skeleton.Web.Configurations.HealthCheck.Checks;
using Net.WebApi.Skeleton.Web.Models.HealthCheck;

namespace Net.WebApi.Skeleton.Web.Configurations.HealthCheck;

public static class HealthCheckConfigs
{
    private static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static IServiceCollection AddHealthCheckConfigs(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<KeycloakCheck>("keycloak");

        return services;
    }

    public static IApplicationBuilder UseHealthCheckConfigs(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks("/api/v1/health/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Name == "liveness",
                ResponseWriter = HealthCheckResponseWriter()
            })
            .UseHealthChecks("/api/v1/health/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Name == "keycloak",
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

