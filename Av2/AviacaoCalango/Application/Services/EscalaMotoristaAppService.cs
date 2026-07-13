using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Domain.Enums;

namespace AviacaoCalango.Application.Services;

public class EscalaMotoristaAppService
{
    private readonly IMotoristaRepository _motoristaRepository;

    public EscalaMotoristaAppService(IMotoristaRepository motoristaRepository)
    {
        _motoristaRepository = motoristaRepository;
    }

    /// <summary>
    /// Busca motoristas disponíveis na estação de origem e valida:
    /// - descanso mínimo de 12h
    /// - limite de 6h ou 400km
    /// - emissão do aviso de 24h
    /// </summary>
    public async Task<IReadOnlyList<Motorista>> BuscarEPrepararEscalaAsync(
        Guid paradaOrigemId,
        DateTimeOffset agora,
        TimeSpan tempoDirecaoPlanejado,
        int kmPlanejados,
        DateTimeOffset dataHoraEmbarque,
        CancellationToken cancellationToken = default)
    {
        var disponiveis = await _motoristaRepository.ListDisponiveisNaParadaAsync(paradaOrigemId, agora, cancellationToken);

        var aptos = new List<Motorista>();

        foreach (var motorista in disponiveis)
        {
            motorista.ValidarDisponibilidade(agora, tempoDirecaoPlanejado: tempoDirecaoPlanejado, kmPlanejados: kmPlanejados);

            // aviso de 24h antes do embarque
            var avisoQuando = dataHoraEmbarque.AddHours(-24);
            if (agora >= avisoQuando && motorista.Status != StatusMotorista.NotificadoParaViagem)
            {
                motorista.EmitirAvisoParaViagem();
                await _motoristaRepository.UpdateAsync(motorista, cancellationToken);
            }

            aptos.Add(motorista);
        }

        return aptos;
    }
}

