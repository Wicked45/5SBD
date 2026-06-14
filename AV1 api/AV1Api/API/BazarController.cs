using AV1Api.Application;
using Microsoft.AspNetCore.Mvc;

namespace AV1Api.API;

[ApiController]
[Route("api/bazar")]
public class BazarController : ControllerBase
{
    private readonly IBazarAppService _appService;

    public BazarController(IBazarAppService appService)
    {
        _appService = appService;
    }

    [HttpGet("pedidos")]
    public async Task<IActionResult> ObterPedidos(CancellationToken cancellationToken)
    {
        var result = await _appService.ObterPedidosAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("reposicao")]
    public async Task<IActionResult> ObterReposicao(CancellationToken cancellationToken)
    {
        var result = await _appService.ObterReposicaoAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost("importar-etl")]
    public async Task<IActionResult> ImportarEtl(CancellationToken cancellationToken)
    {
        await _appService.ImportarEtlAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("processar-estoque")]
    public async Task<IActionResult> ProcessarEstoque(CancellationToken cancellationToken)
    {
        await _appService.ProcessarEstoqueAsync(cancellationToken);
        return NoContent();
    }
}

