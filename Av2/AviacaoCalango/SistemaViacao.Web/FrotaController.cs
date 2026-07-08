namespace SistemaViacao.Web;

using Microsoft.AspNetCore.Mvc;
using SistemaViacao.Core.Interfaces;
using SistemaViacao.UseCases;
using SistemaViacao.UseCases.DTOs;

[ApiController]

[Route("api/[controller]")]
public class FrotaController : ControllerBase
{
    private readonly GestaoFrotaService _service;

    public FrotaController(GestaoFrotaService service)
    {
        _service = service;
    }

    [HttpGet("frota")]
    public async Task<IActionResult> ListarFrota([FromServices] IFrotaRepository repo)
    {
        var frota = await repo.ListarFrota();
        return Ok(frota);
    }


    [HttpPost("viagem")]
    public async Task<IActionResult> RegistrarViagem([FromBody] RegistrarViagemInput input)
    {
        await _service.RegistrarFimDeViagemAsync(input);
        return Ok();
    }
}

