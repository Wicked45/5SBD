using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AviacaoCalango.Api.Controllers;

/// <summary>
/// Endpoints para gerenciamento de rotas.
/// </summary>
[ApiController]
[Route("api/rotas")]
public class RotasController : ControllerBase
{
    private readonly IRotaRepository _rotaRepository;

    public RotasController(IRotaRepository rotaRepository) => _rotaRepository = rotaRepository;

    /// <summary>
    /// Cadastra uma nova rota.
    /// </summary>
    /// <param name="request">Dados da rota.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Rota cadastrada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RotaResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CadastrarRotaRequestDto request, CancellationToken cancellationToken)
    {
        var rota = new Rota(Guid.NewGuid(), request.Nome);
        await _rotaRepository.AddAsync(rota, cancellationToken);

        return CreatedAtAction(nameof(ListarAsync), new { }, new RotaResponseDto(rota.Id, rota.Nome));
    }

    /// <summary>
    /// Lista todas as rotas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista de rotas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<RotaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAsync(CancellationToken cancellationToken)
    {
        var rotas = await _rotaRepository.ListAsync(cancellationToken);
        var result = rotas.Select(r => new RotaResponseDto(r.Id, r.Nome)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Adiciona uma parada à rota.
    /// </summary>
    /// <param name="rotaId">Id da rota.</param>
    /// <param name="request">Dados da parada na rota.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Rota atualizada.</returns>
    [HttpPost("{rotaId:guid}/paradas")]
    [ProducesResponseType(typeof(RotaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdicionarParadaAsync(
        [FromRoute] Guid rotaId,
        [FromBody] AdicionarParadaRequestDto request,
        CancellationToken cancellationToken)
    {
        var rota = await _rotaRepository.GetByIdAsync(rotaId, cancellationToken);
        if (rota is null) return NotFound();

        rota.AdicionarParada(request.ParadaId, request.Sequencia);
        await _rotaRepository.UpdateAsync(rota, cancellationToken);

        return Ok(new RotaResponseDto(rota.Id, rota.Nome));
    }
}

public sealed record CadastrarRotaRequestDto(string Nome);

public sealed record AdicionarParadaRequestDto(int Sequencia, Guid ParadaId);

public sealed record RotaResponseDto(Guid Id, string Nome);

