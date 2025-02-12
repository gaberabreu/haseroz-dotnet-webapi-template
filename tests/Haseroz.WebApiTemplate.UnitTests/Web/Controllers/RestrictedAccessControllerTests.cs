using Haseroz.WebApiTemplate.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Controllers;

public class RestrictedAccessControllerTests
{
    private readonly RestrictedAccessController _controller;

    public RestrictedAccessControllerTests()
    {
        _controller = new RestrictedAccessController()
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task Given_RequestToEndpoint_When_DefaultIsCalled_Then_ReturnsNoContent()
    {
        // Act
        var result = await _controller.Default();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Given_RequestToEndpoint_When_ReaderIsCalled_Then_ReturnsNoContent()
    {
        // Act
        var result = await _controller.Reader();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Given_RequestToEndpoint_When_WriterIsCalled_Then_ReturnsNoContent()
    {
        // Act
        var result = await _controller.Writer();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Given_RequestToEndpoint_When_ReaderOrWriterIsCalled_Then_ReturnsNoContent()
    {
        // Act
        var result = await _controller.ReaderOrWriter();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
