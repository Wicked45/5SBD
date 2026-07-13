using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Enums;
using AviacaoCalango.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AviacaoCalango.Api.Controllers;

/// <summary>
/// Endpoints para gerenciamento da frota.
/// </summary>
[ApiController]
[Route("api/frota")]
public class FrotaController : ControllerBase
{
    private readonly IOnibusRepository _onibusRepository;

    public FrotaController(IOnibusRepository onibusRepository)
    {
        _onibusRepository = onibusRepository;
    }

    /// <summary>
    /// Cadastra um novo ônibus na frota.
    /// </summary>
    /// <param name="request">Dados do ônibus.</param>
    /// <returns>Onibus cadastrado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OnibusResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CadastrarOnibusRequestDto request, CancellationToken cancellationToken)
    {
        var onibus = new Onibus(Guid.NewGuid(), request.Tipo, request.Capacidade);
        await _onibusRepository.AddAsync(onibus, cancellationToken);

        return CreatedAtAction(nameof(ListarAsync), new { }, new OnibusResponseDto(onibus.Id, onibus.Tipo, onibus.Status, onibus.Capacidade, onibus.KmDesdeUltimaManutencao));
    }

    /// <summary>
    /// Lista todos os ônibus cadastrados.
    /// </summary>
    /// <returns>Lista de ônibus.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<OnibusResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAsync(CancellationToken cancellationToken)
    {
        var onibus = await _onibusRepository.ListAsync(cancellationToken);
        var result = onibus.Select(o => new OnibusResponseDto(o.Id, o.Tipo, o.Status, o.Capacidade, o.KmDesdeUltimaManutencao)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Registra a quilometragem acumulada do ônibus.
    /// Quando atingir 10.000km desde a última manutenção, o ônibus muda para status EmManutencao.
    /// </summary>
    /// <param name="onibusId">Id do ônibus.</param>
    /// <param name="request">Quilometragem a registrar.</param>
    /// <returns>Onibus atualizado.</returns>
    [HttpPost("{onibusId:guid}/km")]
    [ProducesResponseType(typeof(OnibusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarKmAsync([FromRoute] Guid onibusId, [FromBody] RegistrarKmRequestDto request, CancellationToken cancellationToken)
    {
        var onibus = await _onibusRepository.GetByIdAsync(onibusId, cancellationToken);
        if (onibus is null) return NotFound();

        onibus.RegistrarKm(request.Km);
        await _onibusRepository.UpdateAsync(onibus, cancellationToken);

        return Ok(new OnibusResponseDto(onibus.Id, onibus.Tipo, onibus.Status, onibus.Capacidade, onibus.KmDesdeUltimaManutencao));
    }

    /// <summary>
    /// Registra a manutenção do ônibus (zera km desde última manutenção e retorna para status Disponivel).
    /// </summary>
    /// <param name="onibusId">Id do ônibus.</param>
    /// <returns>Onibus atualizado.</returns>
    [HttpPost("{onibusId:guid}/manutencao")]
    [ProducesResponseType(typeof(OnibusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RealizarManutencaoAsync([FromRoute] Guid onibusId, CancellationToken cancellationToken)
    {
        var onibus = await _onibusRepository.GetByIdAsync(onibusId, cancellationToken);
        if (onibus is null) return NotFound();

        onibus.RealizarManutencao();
        await _onibusRepository.UpdateAsync(onibus, cancellationToken);

        return Ok(new OnibusResponseDto(onibus.Id, onibus.Tipo, onibus.Status, onibus.Capacidade, onibus.KmDesdeUltimaManutencao));
    }
}

public sealed record CadastrarOnibusRequestDto(TipoOnibus Tipo, int Capacidade);

public sealed record RegistrarKmRequestDto(int Km);

public sealed record OnibusResponseDto(
    Guid Id,
    TipoOnibus Tipo,
    StatusOnibus Status,
    int Capacidade,
    int KmDesdeUltimaManutencao);

