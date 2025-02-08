using Microsoft.AspNetCore.Mvc;

namespace Haseroz.WebApiTemplate.Api.Controllers;

/// <summary>
/// Controller with free access, no authentication required.
/// </summary>
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class FreeAccessController : ControllerBase
{
    /// <summary>
    /// Returns no content, indicating free access.
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get()
    {
        return NoContent();
    }
}
