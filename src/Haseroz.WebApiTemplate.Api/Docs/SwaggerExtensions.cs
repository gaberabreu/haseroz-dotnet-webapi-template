using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Haseroz.WebApiTemplate.Api.Docs;
using Haseroz.WebApiTemplate.Api.Docs.OpenApi;
using Haseroz.WebApiTemplate.Api.Identity;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Haseroz.WebApiTemplate.Api.Docs;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerConfigs(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(options =>
            {
                options.IncludeXmlComments();
            })
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureApiDescription>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureKeycloakAuthentication>();
    }

    internal static IApplicationBuilder UseSwaggerConfigs(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        var keycloakConfigs = app.ApplicationServices.GetRequiredService<IOptions<KeycloakConfig>>();

        return app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpper());
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
