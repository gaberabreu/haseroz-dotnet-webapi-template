using System.Net;
using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Haseroz.WebApiTemplate.Web.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Xunit.Abstractions;

namespace Haseroz.WebApiTemplate.IntegrationTests.Docs;

public class SwaggerExtensionsTests(CustomWebApplicationFactory factory, ITestOutputHelper output) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GIVEN_SwaggerIsConfigured_WHEN_EndpointCalled_THEN_ReturnOK()
    {
        // Act
        var response = await _client.ExecuteGetAsync("swagger", output);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GIVEN_SwaggerIsConfigured_WHEN_EndpointCalledForEachVersion_THEN_ReturnOK()
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

    [Fact]
    public void GIVEN_SwaggerIsConfigured_THEN_ApiDescriptionIsAdded()
    {
        // Arrange
        var apiVersionProvider = factory.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var swaggerProvider = factory.Services.GetRequiredService<ISwaggerProvider>();

        var assembly = Assembly.GetAssembly(typeof(Program))!;
        var assemblyTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
        var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

        // Act & Assert
        foreach (var apiVersion in apiVersionProvider.ApiVersionDescriptions)
        {
            var swaggerDocument = swaggerProvider.GetSwagger(apiVersion.GroupName);

            Assert.Equal(assemblyTitle, swaggerDocument.Info.Title);
            Assert.Equal(apiVersion.ApiVersion.ToString(), swaggerDocument.Info.Version);
            Assert.Equal(assemblyDescription, swaggerDocument.Info.Description);
        }
    }

    [Fact]
    public void GIVEN_SwaggerIsConfigured_THEN_KeycloakAuthenticationIsAdded()
    {
        // Arrange
        var apiVersionProvider = factory.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var swaggerProvider = factory.Services.GetRequiredService<ISwaggerProvider>();
        var keycloakConfig = factory.Services.GetRequiredService<IOptions<KeycloakConfig>>();

        // Act & Assert
        foreach (var apiVersion in apiVersionProvider.ApiVersionDescriptions)
        {
            var swaggerDocument = swaggerProvider.GetSwagger(apiVersion.GroupName);

            Assert.Single(swaggerDocument.SecurityRequirements);
            var securityRequirement = swaggerDocument.SecurityRequirements[0];

            Assert.Single(securityRequirement);
            var securityScheme = securityRequirement.First();

            Assert.Single(securityScheme.Value);
            Assert.Equal("Keycloak", securityScheme.Value[0]);

            Assert.Equal(keycloakConfig.Value.AuthorizationUrl, securityScheme.Key.Flows.AuthorizationCode.AuthorizationUrl.ToString());
            Assert.Equal(keycloakConfig.Value.TokenUrl, securityScheme.Key.Flows.AuthorizationCode.TokenUrl.ToString());
        }
    }
}
