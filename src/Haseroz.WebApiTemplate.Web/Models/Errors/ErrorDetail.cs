namespace Haseroz.WebApiTemplate.Web.Models.Errors;

public class ErrorDetail
{
    public required string Type { get; init; }
    public required string Error { get; init; }
    public required string Detail { get; init; }
}
