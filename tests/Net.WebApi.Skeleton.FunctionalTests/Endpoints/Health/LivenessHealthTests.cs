using System.Net;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net.WebApi.Skeleton.FunctionalTests.Utils;
using Net.WebApi.Skeleton.FunctionalTests.Utils.Extensions;
using Net.WebApi.Skeleton.Web.Models.HealthCheck;
using Xunit.Abstractions;

namespace Net.WebApi.Skeleton.FunctionalTests.Endpoints.Health;

public class LivenessHealthTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_ApplicationIsRunning_Then_ReturnOk()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/v1/health/liveness", output);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.DeserializeAsync<HealthCheckResponse>();
        Assert.NotNull(content);
        Assert.Equal(content.Status, HealthStatus.Healthy.ToString());
        Assert.Null(content.Dependencies);
    }
}
