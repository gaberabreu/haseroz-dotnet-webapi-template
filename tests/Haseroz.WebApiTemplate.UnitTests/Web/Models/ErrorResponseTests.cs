using Haseroz.WebApiTemplate.Web.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Models;

public class ErrorResponseTests
{
    [Fact]
    public void GIVEN_ValidProperties_WHEN_CreatingInstance_THEN_ValuesAreSetProperly()
    {
        // Arrange
        var instance = "/error";
        var traceId = "trace-id";

        // Act
        var errorResponse = new ErrorResponse
        {
            Instance = instance,
            TraceId = traceId
        };

        // Assert
        Assert.Equal(instance, errorResponse.Instance);
        Assert.Equal(traceId, errorResponse.TraceId);
    }

    [Fact]
    public void GIVEN_HttpContext_WHEN_FromContextIsCalled_THEN_ReturnsWithTraceIdAndInstance()
    {
        // Arrange
        var instance = "/error";
        var traceId = "trace-id";

        var httpContextMock = new Mock<HttpContext>();
        var requestMock = new Mock<HttpRequest>();
        httpContextMock.Setup(h => h.TraceIdentifier).Returns(traceId);
        requestMock.Setup(r => r.Path).Returns(instance);
        httpContextMock.Setup(h => h.Request).Returns(requestMock.Object);

        // Act
        var errorResponse = ErrorResponse.FromContext(httpContextMock.Object);

        // Assert
        Assert.Equal(instance, errorResponse.Instance);
        Assert.Equal(traceId, errorResponse.TraceId);
    }

    [Fact]
    public void GIVEN_AnErrorResponse_WHEN_WithErrorIsCalled_THEN_ItContainsTheProvidedError()
    {
        // Arrange
        var errorResponse = new ErrorResponse
        {
            Instance = "/error",
            TraceId = "trace-id"
        };

        var errorDetail = new ErrorDetail
        {
            Type = "Validation",
            Error = "ERR001",
            Detail = "An error occurred"
        };

        // Act
        errorResponse.WithError(errorDetail);

        // Assert
        Assert.Single(errorResponse.Errors);
        Assert.Contains(errorDetail, errorResponse.Errors);
    }
}