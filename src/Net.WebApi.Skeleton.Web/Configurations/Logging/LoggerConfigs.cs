using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace Net.WebApi.Skeleton.Web.Configurations.Logging;

internal static class LoggerConfigs
{
    internal static IHostBuilder AddLoggerConfigs(this IHostBuilder host)
    {
        return host.UseSerilog((context, serviceProvider, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers([new DbUpdateExceptionDestructurer()]));
        });
    }

    internal static IApplicationBuilder UseLoggerConfigs(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });
    }
}
