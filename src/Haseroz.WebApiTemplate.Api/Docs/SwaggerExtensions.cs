using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Haseroz.WebApiTemplate.Api.Docs;

internal static class SwaggerExtensions
{
    internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        return app.UseSwagger(options =>
            {

            })
            .UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpper());
                }
            });
    }

    internal static IServiceCollection AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureApiDescription>();

        return services;
    }

    private static void IncludeXmlComments(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
}
