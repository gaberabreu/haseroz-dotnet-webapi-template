using System.Net;
using Haseroz.WebApiTemplate.Web.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests.Web.HealthCheck;

public class HealthCheckExtensionsTests(CustomWebApplicationFactory factory, ITestOutputHelper output) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GIVEN_HealthCheckIsConfigured_WHEN_EndpointCalled_THEN_ReturnOK()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/health/liveness", output);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.DeserializeAsync<HealthCheckResponse>();
        Assert.NotNull(content);
        Assert.Equal(content.Status, HealthStatus.Healthy.ToString());
    }
}
