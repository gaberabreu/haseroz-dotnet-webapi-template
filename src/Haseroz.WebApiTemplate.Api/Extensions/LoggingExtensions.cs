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

    internal static IHostBuilder UseSerilogWithDefaults(this  IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, serviceProvider, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider)
                .WithDefaults();
        });
    }

    internal static LoggerConfiguration WithDefaults(this LoggerConfiguration loggerConfig)
    {
        return loggerConfig
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers([new DbUpdateExceptionDestructurer()]))
            .WriteTo.Console(outputTemplate: LogTemplate, theme: AnsiConsoleTheme.Code);
    }

    internal static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app) 
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });
    }
}
