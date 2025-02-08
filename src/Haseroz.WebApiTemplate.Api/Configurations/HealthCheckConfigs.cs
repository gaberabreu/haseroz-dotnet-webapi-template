namespace Haseroz.WebApiTemplate.Api.Configurations;

internal static class HealthCheckConfigs
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
