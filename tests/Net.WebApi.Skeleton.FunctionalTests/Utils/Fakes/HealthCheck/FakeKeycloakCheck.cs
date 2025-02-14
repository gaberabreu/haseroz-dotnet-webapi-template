﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Net.WebApi.Skeleton.FunctionalTests.Utils.Fakes.HealthCheck;

public class FakeKeycloakCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Mocked Keycloak is always healthy."));
    }
}
