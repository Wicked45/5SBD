using AviacaoCalango.Application.Services;
using AviacaoCalango.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AviacaoCalango.Api.Controllers;

/// <summary>
/// Endpoints para vendas por canal (Web e Guichê).
/// </summary>
[ApiController]
[Route("api/vendas")]
public class VendasController : ControllerBase
{
    private readonly VendaAppService _vendaAppService;

    public VendasController(VendaAppService vendaAppService) => _vendaAppService = vendaAppService;

    /// <summary>
    /// Realiza uma venda pela web.
    /// Aceita apenas Pix, Crédito ou Débito.
    /// </summary>
    /// <param name="request">Dados da compra.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Resultado da venda.</returns>
    [HttpPost("web")]
    [ProducesResponseType(typeof(VendaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ComprarWebAsync([FromBody] VendaWebRequestDto request, CancellationToken cancellationToken)
    {
        if (request.CanalPagamento is TipoPagamento.Dinheiro)
            return BadRequest("Canal Web não aceita pagamento em Dinheiro.");

        // Nesta fase, o VendaAppService ainda não possui orquestração completa de domínio/EF.
        // Mantemos o endpoint apenas como contrato; a lógica de alocação será aplicada quando a compra/passagem estiver completa.
        // Retornamos uma resposta determinística de assento usando a lógica existente.
        // Observação: preço total e criação de Passagem serão implementados na Fase seguinte.
        var capacidade = 32; // placeholder técnico (precisa vir do ônibus associado à viagem).
        var assentosOcupados = (IReadOnlyCollection<int>)Array.Empty<int>();
        var (assentoAlocado, _) = _vendaAppService.Comprar(capacidade, assentosOcupados, request.AssentoSolicitado);

        return CreatedAtAction(nameof(ComprarWebAsync), new { }, new VendaResponseDto(Guid.Empty, 0m, assentoAlocado));
    }

    /// <summary>
    /// Realiza uma venda no guichê.
    /// Aceita apenas Dinheiro.
    /// </summary>
    /// <param name="request">Dados da compra.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Resultado da venda.</returns>
    [HttpPost("guiche")]
    [ProducesResponseType(typeof(VendaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ComprarGuicheAsync([FromBody] VendaGuicheRequestDto request, CancellationToken cancellationToken)
    {
        if (request.CanalPagamento is not TipoPagamento.Dinheiro)
            return BadRequest("Canal Guichê aceita apenas pagamento em Dinheiro.");

        var capacidade = 32; // placeholder técnico (precisa vir do ônibus associado à viagem).
        var assentosOcupados = (IReadOnlyCollection<int>)Array.Empty<int>();
        var (assentoAlocado, _) = _vendaAppService.Comprar(capacidade, assentosOcupados, request.AssentoSolicitado);

        return CreatedAtAction(nameof(ComprarGuicheAsync), new { }, new VendaResponseDto(Guid.Empty, 0m, assentoAlocado));
    }
}

public sealed record VendaWebRequestDto(
    Guid ViagemId,
    Guid OrigemParadaId,
    Guid DestinoParadaId,
    Guid PassageiroId,
    TipoPagamento CanalPagamento,
    int? AssentoSolicitado,
    DateTimeOffset DataCompra);

public sealed record VendaGuicheRequestDto(
    Guid ViagemId,
    Guid OrigemParadaId,
    Guid DestinoParadaId,
    Guid PassageiroId,
    TipoPagamento CanalPagamento,
    int? AssentoSolicitado,
    DateTimeOffset DataCompra);

public sealed record VendaResponseDto(Guid PassagemId, decimal PrecoTotal, int AssentoFinal);

