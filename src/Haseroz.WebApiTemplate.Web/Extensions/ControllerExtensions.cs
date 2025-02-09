using Asp.Versioning;
using Haseroz.WebApiTemplate.Web.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Haseroz.WebApiTemplate.Web.Extensions;

internal static class ControllerExtensions
{
    internal static IServiceCollection AddControllerConfigs(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning()
            .AddNamingConventions()
            .AddHttpClient()
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
        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
            options.ConstraintMap["transform-to-kebab"] = typeof(KebabCaseTransformer);
        });
    }
}