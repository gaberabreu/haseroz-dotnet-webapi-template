namespace Haseroz.WebApiTemplate.Web.HealthCheck;

public class HealthCheckResponse
{
    public required string Status { get; init; }
    public double TotalDuration { get; init; }
    public List<HealthCheckDependency>? Dependencies { get; init; }
}
