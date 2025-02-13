using Net.WebApi.Skeleton.Web.Models.Errors;

namespace Net.WebApi.Skeleton.UnitTests.Web.Models.Errors;

public class ErrorDetailTests
{
    [Fact]
    public void Given_ValidProperties_When_CreatingInstance_Then_ValuesAreSetProperly()
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
