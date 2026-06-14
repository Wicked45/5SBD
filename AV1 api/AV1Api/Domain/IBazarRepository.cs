using AV1Api.Domain.Entities;

namespace AV1Api.Domain;

public interface IBazarRepository
{
    Task<List<Pedido>> ObterPedidosAsync(CancellationToken cancellationToken = default);
    Task<List<CompraReposicao>> ObterItensReposicaoAsync(CancellationToken cancellationToken = default);

    Task ExecutarImportarEtlAsync(CancellationToken cancellationToken = default);
    Task ExecutarProcessarEstoqueAsync(CancellationToken cancellationToken = default);
}

