﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net.WebApi.Skeleton.Web.Models.HealthCheck;

namespace Net.WebApi.Skeleton.UnitTests.Web.Models.HealthCheck;

public class HealthCheckResponseTests
{
    [Fact]
    public void Given_ValidProperties_When_CreatingInstance_Then_ValuesAreSetProperly()
    {
        // Arrange
        var status = HealthStatus.Healthy.ToString();
        var totalDuration = 7.12d;

        // Act
        var healthCheckResponse = new HealthCheckResponse
        {
            Status = status,
            TotalDuration = totalDuration
        };

        // Assert
        Assert.Equal(status, healthCheckResponse.Status);
        Assert.Equal(totalDuration, healthCheckResponse.TotalDuration);
    }
}
