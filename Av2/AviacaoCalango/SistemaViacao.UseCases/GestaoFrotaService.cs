using SistemaViacao.Core.Interfaces;
using SistemaViacao.UseCases.DTOs;

namespace SistemaViacao.UseCases;

public class GestaoFrotaService
{
    private readonly IFrotaRepository _frotaRepository;

    public GestaoFrotaService(IFrotaRepository frotaRepository)
    {
        _frotaRepository = frotaRepository;
    }

    public async Task RegistrarFimDeViagemAsync(RegistrarViagemInput input)
    {
        var motorista = await _frotaRepository.ObterMotoristaPorId(input.MotoristaId);
        if (motorista is null)
            throw new InvalidOperationException($"Motorista não encontrado: {input.MotoristaId}");

        var onibus = await _frotaRepository.ObterOnibusPorPlaca(input.PlacaOnibus);
        if (onibus is null)
            throw new InvalidOperationException($"Ônibus não encontrado: {input.PlacaOnibus}");

        motorista.RegistrarTurno(input.HorasViajadas, input.KmPercorridos);
        onibus.RegistrarKmViagem(input.KmPercorridos);

        await _frotaRepository.SalvarAlteracoes();
    }
}

