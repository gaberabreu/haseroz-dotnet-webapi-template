using Haseroz.WebApiTemplate.Web.Models.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Models.HealthCheck;

public class HealthCheckDependencyTests
{
    [Fact]
    public void Given_ValidProperties_When_CreatingInstance_Then_ValuesAreSetProperly()
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