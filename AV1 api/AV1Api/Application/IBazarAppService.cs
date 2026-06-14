using AV1Api.Domain.Entities;

namespace AV1Api.Application;

public interface IBazarAppService
{
    Task<List<Pedido>> ObterPedidosAsync(CancellationToken cancellationToken = default);
    Task<List<CompraReposicao>> ObterReposicaoAsync(CancellationToken cancellationToken = default);

    Task ImportarEtlAsync(CancellationToken cancellationToken = default);
    Task ProcessarEstoqueAsync(CancellationToken cancellationToken = default);
}

