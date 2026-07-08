using SistemaViacao.Core;
using SistemaViacao.Core.Interfaces;
using SistemaViacao.UseCases.DTOs;

namespace SistemaViacao.UseCases;

public class VendasService
{
    private readonly IVendasRepository _vendasRepository;
    private readonly IFrotaRepository _frotaRepository;

    public VendasService(IVendasRepository vendasRepository, IFrotaRepository frotaRepository)
    {
        _vendasRepository = vendasRepository;
        _frotaRepository = frotaRepository;
    }

    public async Task VenderPassagemAsync(ComprarPassagemInput input)
    {
        var onibus = await _frotaRepository.ObterOnibusPorPlaca(input.PlacaOnibus);
        if (onibus is null)
            throw new InvalidOperationException($"Ônibus não encontrado: {input.PlacaOnibus}");

        var rota = (await _vendasRepository.ListarRotas())
            .FirstOrDefault(r => r.Id == input.RotaId);

        if (rota is null)
            throw new InvalidOperationException($"Rota não encontrada: {input.RotaId}");

        var passagem = new Passagem(
            id: input.Id,
            rotaId: input.RotaId,
            nomePassageiro: input.NomePassageiro,
            dataViagem: input.DataViagem,
            tipoPagamento: input.TipoPagamento);

        passagem.CalcularValor(onibus, input.DiasAntecedencia, rota);

        await _vendasRepository.SalvarPassagem(passagem);
    }
}

