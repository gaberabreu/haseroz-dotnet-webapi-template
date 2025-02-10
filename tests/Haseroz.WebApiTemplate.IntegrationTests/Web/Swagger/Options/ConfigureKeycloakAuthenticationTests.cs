using Asp.Versioning.ApiExplorer;
using Haseroz.WebApiTemplate.Web.Configurations.Security;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Haseroz.WebApiTemplate.IntegrationTests.Web.Swagger.Options;

public class ConfigureKeycloakAuthenticationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void Given_SwaggerIsConfigured_Then_KeycloakAuthenticationIsAdded()
    {
        // Arrange
        var apiVersionProvider = factory.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var swaggerProvider = factory.Services.GetRequiredService<ISwaggerProvider>();
        var keycloakConfig = factory.Services.GetRequiredService<IOptions<KeycloakSettings>>();

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
