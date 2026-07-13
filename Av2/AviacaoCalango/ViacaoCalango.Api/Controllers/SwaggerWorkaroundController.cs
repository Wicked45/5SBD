using Microsoft.AspNetCore.Mvc;

namespace AviacaoCalango.Api.Controllers;

/// <summary>
/// Endpoint dummy para garantir que a aplicação suba mesmo quando Swagger não puder ser inicializado.
/// </summary>
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

