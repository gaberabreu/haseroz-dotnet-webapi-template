using Haseroz.WebApiTemplate.Web.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Models;

public class HealthCheckResponseTests
{
    [Fact]
    public void GIVEN_ValidProperties_WHEN_CreatingInstance_THEN_ValuesAreSetProperly()
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
