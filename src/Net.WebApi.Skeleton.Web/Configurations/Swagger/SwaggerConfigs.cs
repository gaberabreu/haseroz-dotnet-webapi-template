using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Net.WebApi.Skeleton.Web.Configurations.Security;
using Net.WebApi.Skeleton.Web.Configurations.Swagger;
using Net.WebApi.Skeleton.Web.Configurations.Swagger.Filters;
using Net.WebApi.Skeleton.Web.Configurations.Swagger.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Net.WebApi.Skeleton.Web.Configurations.Swagger;

public static class SwaggerConfigs
{
    public static IServiceCollection AddSwaggerConfigs(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(options =>
            {
                options.IncludeXmlComments();
                options.DocumentFilter<HealthCheckSwaggerFilter>();
            })
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureApiDescription>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureKeycloakAuthentication>();
    }

    public static IApplicationBuilder UseSwaggerConfigs(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        var keycloakConfigs = app.ApplicationServices.GetRequiredService<IOptions<KeycloakSettings>>();

        return app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                foreach (var groupName in provider.ApiVersionDescriptions.Select(d => d.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpper());
                }

                options.OAuthClientId(keycloakConfigs.Value.ClientId);
                options.OAuthClientSecret(keycloakConfigs.Value.ClientSecret);
            });
    }

    private static void IncludeXmlComments(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
}