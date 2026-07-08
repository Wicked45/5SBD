using Microsoft.AspNetCore.Mvc;
using ViacaoCalangoApi.Application.Services;

namespace ViacaoCalangoApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrotaController : ControllerBase
{
    private readonly ViacaoService _service;

    public FrotaController(ViacaoService service)
    {
        _service = service;
    }

    [HttpGet("motoristas-disponiveis")]
    public async Task<IActionResult> ListarMotoristasAptosAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.ListarMotoristasAptosAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("onibus")]
    public async Task<IActionResult> ListarFrotaAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.ListarFrotaAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("registrar-viagem/{motoristaId}/{placa}")]
    public async Task<IActionResult> RegistrarFimDeViagemAsync(
        [FromRoute] int motoristaId,
        [FromRoute] string placa,
        [FromQuery] int horas,
        [FromQuery] int km,
        CancellationToken cancellationToken)
    {
        try
        {
            await _service.RegistrarFimDeViagemAsync(motoristaId, placa, horas, km);
            return Ok("Viagem registrada com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


