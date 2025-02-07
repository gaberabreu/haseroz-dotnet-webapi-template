using Microsoft.AspNetCore.Mvc;

namespace Haseroz.WebApiTemplate.Api.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class CardsController : ControllerBase
{
    [HttpGet("{cardId:guid}")]
    public async Task<IActionResult> GetCardById([FromRoute] Guid cardId)
    {
        await Task.CompletedTask;

        return Ok(new
        {
            CardId = cardId
        });
    }
}
