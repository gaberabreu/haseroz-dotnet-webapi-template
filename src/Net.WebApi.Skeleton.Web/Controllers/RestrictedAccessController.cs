using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Net.WebApi.Skeleton.Web.Controllers;

/// <summary>
/// Controller that requires authentication.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class RestrictedAccessController : ControllerBase
{
    /// <summary>
    /// Returns no content, indicating restricted access.
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Default()
    {
        await Task.CompletedTask;
        return NoContent();
    }

    /// <summary>
    /// Returns no content, indicating restricted access.
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet("Reader")]
    [Authorize(Roles = "reader")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Reader()
    {
        await Task.CompletedTask;
        return NoContent();
    }

    /// <summary>
    /// Returns no content, indicating restricted access.
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet("Writer")]
    [Authorize(Roles = "writer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Writer()
    {
        await Task.CompletedTask;
        return NoContent();
    }

    /// <summary>
    /// Returns no content, indicating restricted access.
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet("Reader-Or-Writer")]
    [Authorize(Roles = "reader,writer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ReaderOrWriter()
    {
        await Task.CompletedTask;
        return NoContent();
    }
}