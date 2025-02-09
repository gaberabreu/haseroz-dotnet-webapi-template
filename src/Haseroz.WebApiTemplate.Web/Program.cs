using Haseroz.WebApiTemplate.Web.Docs;
using Haseroz.WebApiTemplate.Web.Extensions;
using Haseroz.WebApiTemplate.Web.Identity;
using Haseroz.WebApiTemplate.Web.Services;

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

public partial class Program
{
    protected Program()
    {
    }
}