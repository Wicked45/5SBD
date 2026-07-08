namespace SistemaViacao.Core.Interfaces;

public interface IFrotaRepository
{
    Task<Motorista?> ObterMotoristaPorId(int id);
    Task<Onibus?> ObterOnibusPorPlaca(string placa);
    Task<List<Onibus>> ListarFrota();
    Task<List<Motorista>> ListarMotoristasAptos();
    Task SalvarAlteracoes();
}

