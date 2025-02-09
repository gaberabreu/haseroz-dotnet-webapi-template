namespace Haseroz.WebApiTemplate.Web.Models;

public class HealthCheckResponse
{
    public required string Status { get; init; }
    public double TotalDuration { get; init; }
}

