using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Net.WebApi.Skeleton.Web.Configurations.Transformers;

namespace Net.WebApi.Skeleton.Web.Configurations.Controllers;

public static class ControllersConfigs
{
    public static IServiceCollection AddControllersConfigs(this IServiceCollection services)
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
