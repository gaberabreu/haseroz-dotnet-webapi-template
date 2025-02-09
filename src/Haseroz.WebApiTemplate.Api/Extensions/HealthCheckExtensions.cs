namespace Haseroz.WebApiTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    internal static IServiceCollection AddHealthChecksConfigs(this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

    internal static IApplicationBuilder UseHealthChecksConfigs(this IApplicationBuilder app)
    {
        return app.UseHealthChecks("/api/health");
    }
}
