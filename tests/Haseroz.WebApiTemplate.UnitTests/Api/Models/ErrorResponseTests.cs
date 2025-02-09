using Haseroz.WebApiTemplate.Api.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Haseroz.WebApiTemplate.UnitTests.Api.Models;

public class ErrorResponseTests
{
    [Fact]
    public void Given_Constructor_When_Instantiate_Then_ShouldSetPropertiesCorrectly()
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
    public void Given_FromContext_When_Instantiate_Then_ShouldSetPropertiesCorrectly()
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
    public void Given_WithError_When_AddingAnError_Then_ShouldContainTheError()
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