using Asp.Versioning;
using Haseroz.WebApiTemplate.Web.Configurations.Transformers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Haseroz.WebApiTemplate.Web.Configurations.Controllers;

internal static class ControllersConfigs
{
    internal static IServiceCollection AddControllersConfigs(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseTransformer()));
        });

        services.AddEndpointsApiExplorer();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.ConstraintMap["transform-to-kebab"] = typeof(KebabCaseTransformer);
        });

        services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

}
