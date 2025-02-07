using Haseroz.WebApiTemplate.Api.Docs;
using Haseroz.WebApiTemplate.Api.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WithDefaults()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilogWithDefaults();

builder.Services.AddDependencies();

try
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
    }

    app
        .UseHealthChecks()
        .UseSerilogRequestLogging()
        .UseRouting()
        .UseExceptionHandler(_ => { })
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(options => options.MapControllers());

    Log.Information("Starting web application");

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

