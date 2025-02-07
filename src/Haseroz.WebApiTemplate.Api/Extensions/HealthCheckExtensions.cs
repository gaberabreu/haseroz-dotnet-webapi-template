namespace Haseroz.WebApiTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    internal static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        return app.UseHealthChecks("/api/health");
    }
}
