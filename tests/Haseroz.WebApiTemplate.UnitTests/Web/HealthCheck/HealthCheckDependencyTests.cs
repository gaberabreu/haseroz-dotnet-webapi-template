using Haseroz.WebApiTemplate.Web.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Haseroz.WebApiTemplate.UnitTests.Web.HealthCheck;

public class HealthCheckDependencyTests
{
    [Fact]
    public void GIVEN_ValidProperties_WHEN_CreatingInstance_THEN_ValuesAreSetProperly()
    {
        // Arrange
        var name = "health-dependency";
        var status = HealthStatus.Healthy.ToString();
        var duration = 7.12d;
        var description = "health-dependency-description";
        var errorMessage = "health-dependency-error-message";

        // Act
        var healthCheckResponse = new HealthCheckDependency
        {
            Name = name,
            Status = status,
            Duration = duration,
            Description = description,
            ErrorMessage = errorMessage
        };

        // Assert
        Assert.Equal(name, healthCheckResponse.Name);
        Assert.Equal(status, healthCheckResponse.Status);
        Assert.Equal(duration, healthCheckResponse.Duration);
        Assert.Equal(description, healthCheckResponse.Description);
        Assert.Equal(errorMessage, healthCheckResponse.ErrorMessage);
    }
}