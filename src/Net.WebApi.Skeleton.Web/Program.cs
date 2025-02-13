using Net.WebApi.Skeleton.Web.Configurations;
using Net.WebApi.Skeleton.Web.Configurations.Controllers;
using Net.WebApi.Skeleton.Web.Configurations.HealthCheck;
using Net.WebApi.Skeleton.Web.Configurations.HttpClient;
using Net.WebApi.Skeleton.Web.Configurations.Logging;
using Net.WebApi.Skeleton.Web.Configurations.Security;
using Net.WebApi.Skeleton.Web.Configurations.Swagger;
using Net.WebApi.Skeleton.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggerConfigs();

builder.Services.AddControllersConfigs();
builder.Services.AddHttpClientConfigs();
builder.Services.AddDependencyInjection();
builder.Services.AddAuthenticationConfigs(builder.Configuration);
builder.Services.AddAuthorizationConfigs();
builder.Services.AddSwaggerConfigs();
builder.Services.AddHealthCheckConfigs();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfigs();
}

app.UseHealthCheckConfigs();
app.UseLoggerConfigs();
app.UseRouting();
app.UseExceptionHandler(_ => { });
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

public partial class Program
{
    protected Program()
    {
    }
}