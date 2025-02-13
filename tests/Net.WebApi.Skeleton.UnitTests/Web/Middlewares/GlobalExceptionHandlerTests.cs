using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Net.WebApi.Skeleton.Web.Middlewares;
using Net.WebApi.Skeleton.Web.Models.Errors;

namespace Net.WebApi.Skeleton.UnitTests.Web.Middlewares;

public class GlobalExceptionHandlerTests
{
    private readonly Mock<ILogger<GlobalExceptionHandler>> _loggerMock;
    private readonly GlobalExceptionHandler _exceptionHandler;

    public GlobalExceptionHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GlobalExceptionHandler>>();
        _exceptionHandler = new GlobalExceptionHandler(_loggerMock.Object);
    }

    [Fact]
    public async Task Given_AnException_WhenHandling_Then_ReturnInternalServerError()
    {
        // Arrange
        var instance = "/error";
        var traceId = "trace-id";

        var context = new DefaultHttpContext()
        {
            TraceIdentifier = traceId,
        };
        context.Request.Path = instance;
        context.Response.Body = new MemoryStream();

        var exception = new Exception("Test exception");

        // Act
        var result = await _exceptionHandler.TryHandleAsync(context, exception, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", context.Response.ContentType);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var jsonResponse = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(errorResponse);
        Assert.Equal(instance, errorResponse.Instance);
        Assert.Equal(traceId, errorResponse.TraceId);

        Assert.Single(errorResponse.Errors);
        Assert.Equal("InternalServerError", errorResponse.Errors[0].Type);
        Assert.Equal("Internal Server Error", errorResponse.Errors[0].Error);
        Assert.Equal("An unexpected error ocurred. Please, try again later.", errorResponse.Errors[0].Detail);
    }
}
