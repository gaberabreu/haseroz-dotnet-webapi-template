using Haseroz.WebApiTemplate.Api.Models;

namespace Haseroz.WebApiTemplate.UnitTests.Api.Models;

public class ErrorDetailTests
{
    [Fact]
    public void Given_Constructor_When_Instantiate_Then_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var type = "ValidationError";
        var error = "ERR001";
        var detail = "An error occurred.";

        // Act
        var errorDetail = new ErrorDetail
        {
            Type = type,
            Error = error,
            Detail = detail
        };

        // Assert
        Assert.Equal(type, errorDetail.Type);
        Assert.Equal(error, errorDetail.Error);
        Assert.Equal(detail, errorDetail.Detail);
    }
}
