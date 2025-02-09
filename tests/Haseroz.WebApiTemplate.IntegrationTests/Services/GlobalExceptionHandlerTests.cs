using System.Net;
using Haseroz.WebApiTemplate.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests.Services;

public class GlobalExceptionHandlerTests(CustomWebApplicationFactory factory, ITestOutputHelper output) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.WithWebHostBuilder(builder =>
        {
            builder.Configure(app =>
            {
                app.UseExceptionHandler(_ => { });
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/error", _ => throw new Exception(ExceptionMessage));
                });
            });
        }).CreateClient();

    private const string ExceptionMessage = "An unexpected error.";

    [Fact]
    public async Task GIVEN_AnUnexpectedException_WHEN_CallingEndpoint_THEN_ReturnFormattedErrorResponse()
    {
        // Act
        var response = await _client.ExecuteGetAsync("/error", output);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        var content = await response.DeserializeAsync<ErrorResponse>();
        Assert.NotNull(content);
        Assert.Equal("/error", content.Instance);
        Assert.NotEmpty(content.TraceId);
        Assert.Single(content.Errors);

        var error = content.Errors.First();
        Assert.Equal("InternalServerError", error.Type);
        Assert.Equal("Internal Server Error", error.Error);
        Assert.Equal("An unexpected error ocurred. Please, try again later.", error.Detail);
    }
}
