using Haseroz.WebApiTemplate.Web.Configurations;
using Haseroz.WebApiTemplate.Web.Configurations.Controllers;
using Haseroz.WebApiTemplate.Web.Configurations.HealthCheck;
using Haseroz.WebApiTemplate.Web.Configurations.HttpClient;
using Haseroz.WebApiTemplate.Web.Configurations.Logging;
using Haseroz.WebApiTemplate.Web.Configurations.Security;
using Haseroz.WebApiTemplate.Web.Configurations.Swagger;
using Haseroz.WebApiTemplate.Web.Middlewares;

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