using Microsoft.AspNetCore.Mvc;
namespace AviacaoCalango.Api.Controllers;


/// <summary>
/// Painel do motorista: manifesto em tempo real para embarque/desembarque na parada atual.
/// </summary>
[ApiController]
[Route("api/painel")]
public class PainelController : ControllerBase
{
    /// <summary>
    /// Retorna a lista de passageiros que devem embarcar e desembarcar na parada atual.
    /// </summary>
    /// <param name="viagemId">Id da viagem.</param>
    /// <param name="paradaAtualId">Id da parada atual.</param>
    /// <returns>Manifesto da parada atual.</returns>
    [HttpGet("viagem/{viagemId:guid}/manifesto")]
    [ProducesResponseType(typeof(List<ManifestoItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ManifestoAsync(
        [FromRoute] Guid viagemId,
        [FromQuery] Guid paradaAtualId,
        CancellationToken cancellationToken)
    {
        // Placeholder contratual: o manifesto em tempo real depende de modelagem completa de Viagem/Passagem x “ação”
        // (embarque/desembarque) e uma consulta específica. A implementação será adicionada na sequência.
        return Ok(new List<ManifestoItemDto>());

    }
}

public sealed record ManifestoItemDto(Guid PassagemId, Guid PassageiroId, string Acao, int Assento);

