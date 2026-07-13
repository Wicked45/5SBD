using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Domain.Repositories;

public interface IMotoristaRepository
{
    Task<Motorista?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Motorista>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retorna motoristas disponíveis (ex.: em estação e aptos para alocação).
    /// A regra exata será implementada no repositório.
    /// </summary>
    Task<IReadOnlyList<Motorista>> ListDisponiveisNaParadaAsync(Guid paradaId, DateTimeOffset agora, CancellationToken cancellationToken = default);

    Task AddAsync(Motorista motorista, CancellationToken cancellationToken = default);
    Task UpdateAsync(Motorista motorista, CancellationToken cancellationToken = default);
}

