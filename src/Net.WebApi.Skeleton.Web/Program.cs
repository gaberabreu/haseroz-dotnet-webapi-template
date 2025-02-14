using Net.WebApi.Skeleton.Infrastructure;
using Net.WebApi.Skeleton.Web.Extensions.Controllers;
using Net.WebApi.Skeleton.Web.Extensions.HealthCheck;
using Net.WebApi.Skeleton.Web.Extensions.Logging;
using Net.WebApi.Skeleton.Web.Extensions.Security;
using Net.WebApi.Skeleton.Web.Extensions.Swagger;
using Net.WebApi.Skeleton.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggerConfigs();

builder.Services.AddControllersConfigs();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructureServices();
builder.Services.AddAuthenticationConfigs(builder.Configuration);
builder.Services.AddAuthorizationConfigs();
builder.Services.AddSwaggerConfigs();
builder.Services.AddHealthCheckConfigs(builder.Configuration);
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