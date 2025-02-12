namespace Haseroz.WebApiTemplate.Web.Models.HealthCheck;

public class HealthCheckDependency
{
    public required string Name { get; init; }
    public required string Status { get; init; }
    public double Duration { get; init; }
    public string? Description { get; init; }
    public string? ErrorMessage { get; init; }
}
