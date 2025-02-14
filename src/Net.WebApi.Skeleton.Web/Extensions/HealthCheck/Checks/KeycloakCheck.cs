using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Net.WebApi.Skeleton.Web.Extensions.Security;

namespace Net.WebApi.Skeleton.Web.Extensions.HealthCheck.Checks;

public class KeycloakCheck(IOptions<KeycloakSettings> keycloakOptions, HttpClient httpClient) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.GetAsync(keycloakOptions.Value.WellKnownConfigUrl, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("Keycloak is available.");
            }

            return HealthCheckResult.Unhealthy("Keycloak is unreachable.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Keycloak connection failed.", ex);
        }
    }
}
