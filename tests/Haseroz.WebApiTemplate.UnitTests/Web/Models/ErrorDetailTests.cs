using Haseroz.WebApiTemplate.Web.Models;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Models;

public class ErrorDetailTests
{
    [Fact]
    public void GIVEN_ValidProperties_WHEN_CreatingInstance_THEN_ValuesAreSetProperly()
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
