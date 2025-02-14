﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.WebApi.Skeleton.Web.Controllers;

namespace Net.WebApi.Skeleton.UnitTests.Web.Controllers;

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
    public async Task Given_RequestToFreeAccessEndpoint_When_GetIsCalled_Then_ReturnsNoContent()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
