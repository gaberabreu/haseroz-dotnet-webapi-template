using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Net.WebApi.Skeleton.IntegrationTests.Web.Extensions.Swagger.Filters;

public class HealthCheckSwaggerFilterTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void Given_SwaggerIsConfigured_Then_HealthCheckAreAdded()
    {
        // Arrange
        var apiVersionProvider = factory.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var swaggerProvider = factory.Services.GetRequiredService<ISwaggerProvider>();

        // Act & Assert
        foreach (var apiVersion in apiVersionProvider.ApiVersionDescriptions)
        {
            var swaggerDocument = swaggerProvider.GetSwagger(apiVersion.GroupName);

            var hasLivenessEndpoint = swaggerDocument.Paths.ContainsKey("/api/v1/health/liveness");
            var hasReadinessEndpoint = swaggerDocument.Paths.ContainsKey("/api/v1/health/readiness");
            var hasHealthCheckDependencySchema = swaggerDocument.Components.Schemas.ContainsKey("HealthCheckDependency");
            var hasHealthCheckResponseSchema = swaggerDocument.Components.Schemas.ContainsKey("HealthCheckResponse");

            Assert.True(hasLivenessEndpoint);
            Assert.True(hasReadinessEndpoint);
            Assert.True(hasHealthCheckDependencySchema);
            Assert.True(hasHealthCheckResponseSchema);
        }
    }
}
