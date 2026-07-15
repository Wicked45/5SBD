using Microsoft.AspNetCore.Mvc;

namespace AviacaoCalango.Api.Controllers;

[ApiController]
[Route("api/health")] 
public class SwaggerWorkaroundController : ControllerBase
{
    /// <summary>
    /// Health check simples.
    /// </summary>
    /// <returns>Ok.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok" });
}

