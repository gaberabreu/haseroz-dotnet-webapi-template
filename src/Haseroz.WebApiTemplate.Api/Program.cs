using Haseroz.WebApiTemplate.Api.Configurations;
using Haseroz.WebApiTemplate.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilogConfigs();

builder.Services
    .AddControllerConfigs()
    .AddAuthenticationConfigs(builder.Configuration)
    .AddAuthorizationConfigs()
    .AddSwaggerConfigs()
    .AddHealthChecksConfigs()
    .AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfigs();
}

app
    .UseHealthChecksConfigs()
    .UseSerilogRequestLoggingConfigs()
    .UseRouting()
    .UseExceptionHandler(_ => { })
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(options => options.MapControllers());

await app.RunAsync();
