namespace SistemaViacao.Core.Interfaces;

using SistemaViacao.Core;

public interface IVendasRepository
{
    Task SalvarPassagem(Passagem passagem);
    Task<List<Rota>> ListarRotas();
}

