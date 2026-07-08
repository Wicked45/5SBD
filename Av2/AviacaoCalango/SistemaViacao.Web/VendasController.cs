namespace SistemaViacao.Web;

using Microsoft.AspNetCore.Mvc;
using SistemaViacao.Core.Interfaces;
using SistemaViacao.UseCases;
using SistemaViacao.UseCases.DTOs;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly VendasService _service;

    public VendasController(VendasService service)
    {
        _service = service;
    }

    [HttpPost("comprar")]
    public async Task<IActionResult> Comprar([FromBody] ComprarPassagemInput input)
    {
        await _service.VenderPassagemAsync(input);
        return Ok();
    }
}

