using Haseroz.WebApiTemplate.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Controllers;

public class FreeAccessControllerTests
{
    private readonly FreeAccessController _controller;

    public FreeAccessControllerTests()
    {
        _controller = new FreeAccessController()
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task GIVEN_RequestToFreeAccessEndpoint_WHEN_GetIsCalled_THEN_ReturnsNoContent()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
