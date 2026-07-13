using AviacaoCalango.Domain.Enums;
using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AviacaoCalango.Api.Controllers;

/// <summary>
/// Endpoints para gerenciamento e visualização do estado dos motoristas.
/// </summary>
[ApiController]
[Route("api/motoristas")]
public class MotoristasController : ControllerBase
{
    private readonly IMotoristaRepository _motoristaRepository;

    public MotoristasController(IMotoristaRepository motoristaRepository)
    {
        _motoristaRepository = motoristaRepository;
    }

    /// <summary>
    /// Lista todos os motoristas cadastrados.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<MotoristaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAsync(CancellationToken cancellationToken)
    {
        var motoristas = await _motoristaRepository.ListAsync(cancellationToken);
        var result = motoristas.Select(m => new MotoristaResponseDto(m.Id, m.Status, m.ParadaAtualId, m.UltimoFimDescanso, m.UltimoInicioDirecao, m.KmDesdeUltimoDescanso)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Lista motoristas disponíveis para alocação em uma parada.
    /// </summary>
    /// <param name="paradaId">Id da parada de origem.</param>
    /// <param name="agora">Data/hora de referência para as validações de disponibilidade.</param>
    [HttpGet("disponiveis")]
    [ProducesResponseType(typeof(List<MotoristaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarDisponiveisAsync([FromQuery] Guid paradaId, [FromQuery] DateTimeOffset agora, CancellationToken cancellationToken)
    {
        var motoristas = await _motoristaRepository.ListDisponiveisNaParadaAsync(paradaId, agora, cancellationToken);
        var result = motoristas.Select(m => new MotoristaResponseDto(m.Id, m.Status, m.ParadaAtualId, m.UltimoFimDescanso, m.UltimoInicioDirecao, m.KmDesdeUltimoDescanso)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Cadastra um motorista alocado inicialmente em uma parada.
    /// </summary>
    /// <param name="request">Dados do motorista.</param>
    [HttpPost]
    [ProducesResponseType(typeof(MotoristaResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CadastrarMotoristaRequestDto request, CancellationToken cancellationToken)
    {
        var motorista = new Motorista(Guid.NewGuid(), request.ParadaAtualId);
        await _motoristaRepository.AddAsync(motorista, cancellationToken);

        return CreatedAtAction(nameof(ListarAsync), new { }, new MotoristaResponseDto(motorista.Id, motorista.Status, motorista.ParadaAtualId, motorista.UltimoFimDescanso, motorista.UltimoInicioDirecao, motorista.KmDesdeUltimoDescanso));
    }

    /// <summary>
    /// Inativa um motorista.
    /// </summary>
    /// <param name="motoristaId">Id do motorista.</param>
    [HttpPost("{motoristaId:guid}/inativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> InativarAsync([FromRoute] Guid motoristaId, CancellationToken cancellationToken)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(motoristaId, cancellationToken);
        if (motorista is null) return NotFound();

        motorista.Inativar();
        await _motoristaRepository.UpdateAsync(motorista, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Aloca o motorista para a parada informada.
    /// </summary>
    [HttpPost("{motoristaId:guid}/alocar")]
    [ProducesResponseType(typeof(MotoristaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AlocarAsync([FromRoute] Guid motoristaId, [FromBody] AlocarMotoristaRequestDto request, CancellationToken cancellationToken)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(motoristaId, cancellationToken);
        if (motorista is null) return NotFound();

        motorista.AlocarParaParada(request.ParadaId);
        await _motoristaRepository.UpdateAsync(motorista, cancellationToken);

        return Ok(new MotoristaResponseDto(motorista.Id, motorista.Status, motorista.ParadaAtualId, motorista.UltimoFimDescanso, motorista.UltimoInicioDirecao, motorista.KmDesdeUltimoDescanso));
    }
}

public sealed record CadastrarMotoristaRequestDto(Guid ParadaAtualId);

public sealed record AlocarMotoristaRequestDto(Guid ParadaId);

public sealed record MotoristaResponseDto(
    Guid Id,
    StatusMotorista Status,
    Guid ParadaAtualId,
    DateTimeOffset? UltimoFimDescanso,
    DateTimeOffset? UltimoInicioDirecao,
    int KmDesdeUltimoDescanso);

