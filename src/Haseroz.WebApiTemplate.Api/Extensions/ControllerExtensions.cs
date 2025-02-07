using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Haseroz.WebApiTemplate.Api.Extensions;

internal static class ControllerExtensions
{
    internal static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning()
            .AddNamingConventions()
            .AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseTransformer()));
            });
        
        return services;
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    private static IServiceCollection AddNamingConventions(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
            options.ConstraintMap["transform-to-kebab"] = typeof(KebabCaseTransformer);
        });

        return services;
    }
}
