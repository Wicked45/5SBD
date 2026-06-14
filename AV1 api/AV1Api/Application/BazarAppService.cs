using AV1Api.Domain;
using AV1Api.Domain.Entities;

namespace AV1Api.Application;

public class BazarAppService : IBazarAppService
{
    private readonly IBazarRepository _repository;

    public BazarAppService(IBazarRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Pedido>> ObterPedidosAsync(CancellationToken cancellationToken = default)
        => _repository.ObterPedidosAsync(cancellationToken);

    public Task<List<CompraReposicao>> ObterReposicaoAsync(CancellationToken cancellationToken = default)
        => _repository.ObterItensReposicaoAsync(cancellationToken);

    public Task ImportarEtlAsync(CancellationToken cancellationToken = default)
        => _repository.ExecutarImportarEtlAsync(cancellationToken);

    public Task ProcessarEstoqueAsync(CancellationToken cancellationToken = default)
        => _repository.ExecutarProcessarEstoqueAsync(cancellationToken);
}

