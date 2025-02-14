﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net.WebApi.Skeleton.Web.Configurations.HttpClient;

namespace Net.WebApi.Skeleton.Web.Configurations.HealthCheck.Checks;

public class KeycloakCheck(IHttpClientFactory _httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient(HttpClientNames.Keycloak);
            var response = await client.GetAsync(".well-known/openid-configuration", cancellationToken);

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
