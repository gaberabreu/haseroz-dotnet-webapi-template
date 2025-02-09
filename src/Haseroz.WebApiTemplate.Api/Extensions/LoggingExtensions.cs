using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Sinks.SystemConsole.Themes;

namespace Haseroz.WebApiTemplate.Api.Extensions;

internal static class LoggingExtensions
{
    private const string LogTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

    internal static IHostBuilder AddSerilogConfigs(this IHostBuilder host)
    {
        return host.UseSerilog((context, serviceProvider, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers([new DbUpdateExceptionDestructurer()]))
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .WriteTo.Console(outputTemplate: LogTemplate, theme: AnsiConsoleTheme.Code);
        });
    }

    internal static IApplicationBuilder UseSerilogRequestLoggingConfigs(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });
    }
}
