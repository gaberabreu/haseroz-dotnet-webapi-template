namespace Haseroz.WebApiTemplate.Web.Models;

public class ErrorResponse
{
    public required string Instance { get; init; }
    public required string TraceId { get; init; }
    public List<ErrorDetail> Errors { get; init; } = [];

    public static ErrorResponse FromContext(HttpContext context)
    {
        return new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Instance = context.Request.Path,
        };
    }

    public ErrorResponse WithError(ErrorDetail error)
    {
        Errors.Add(error);
        return this;
    }
}
