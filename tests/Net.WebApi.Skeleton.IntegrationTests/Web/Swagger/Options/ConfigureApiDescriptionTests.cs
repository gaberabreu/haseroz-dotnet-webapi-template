using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Net.WebApi.Skeleton.IntegrationTests.Web.Swagger.Options;

public class ConfigureApiDescriptionTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void Given_SwaggerIsConfigured_Then_ApiDescriptionIsAdded()
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
}
