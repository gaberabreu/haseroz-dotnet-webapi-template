using Haseroz.WebApiTemplate.Web.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Haseroz.WebApiTemplate.Web.HealthCheck.Checks;

public class KeycloakReadinessCheck(IOptions<KeycloakConfig> keycloakConfig, IHttpClientFactory _httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(keycloakConfig.Value.WellKnownUrl, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("Keycloak is available.");
            }

            return HealthCheckResult.Unhealthy("Keycloak is unreachable.");
        }
        catch
        {
            return HealthCheckResult.Unhealthy("Keycloak connection failed.");
        }
    }
}
