using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Domain.Repositories;

public interface IRotaRepository
{
    Task<Rota?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Rota>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Rota rota, CancellationToken cancellationToken = default);
    Task UpdateAsync(Rota rota, CancellationToken cancellationToken = default);
}

