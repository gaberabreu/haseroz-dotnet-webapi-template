using System.Net;
using Haseroz.WebApiTemplate.FunctionalTests.Utils;
using Haseroz.WebApiTemplate.FunctionalTests.Utils.Extensions;
using Haseroz.WebApiTemplate.Web.Models.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.FunctionalTests.Endpoints.Health;

public class LivenessHealthTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_ApplicationIsRunning_Then_ReturnOk()
    {
        // Act
        var response = await _client.ExecuteGetAsync("api/health/liveness", output);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.DeserializeAsync<HealthCheckResponse>();
        Assert.NotNull(content);
        Assert.Equal(content.Status, HealthStatus.Healthy.ToString());
        Assert.Null(content.Dependencies);
    }
}
