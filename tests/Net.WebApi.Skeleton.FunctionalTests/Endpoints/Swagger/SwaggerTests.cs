﻿using System.Net;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Net.WebApi.Skeleton.FunctionalTests.Utils;
using Net.WebApi.Skeleton.FunctionalTests.Utils.Extensions;
using Xunit.Abstractions;

namespace Net.WebApi.Skeleton.FunctionalTests.Endpoints.Swagger;

public class SwaggerTests(FunctionalTestFactory factory, ITestOutputHelper output) : IClassFixture<FunctionalTestFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Given_SwaggerIsConfigured_When_Called_Then_ReturnOK()
    {
        // Act
        var response = await _client.ExecuteGetAsync("swagger", output);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Given_SwaggerIsConfigured_When_CalledForEachVersion_Then_ReturnOK()
    {
        // Arrange
        var apiVersionProvider = factory.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Act & Assert
        foreach (var apiVersion in apiVersionProvider.ApiVersionDescriptions)
        {
            var response = await _client.ExecuteGetAsync($"swagger/{apiVersion.GroupName}/swagger.json", output);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
