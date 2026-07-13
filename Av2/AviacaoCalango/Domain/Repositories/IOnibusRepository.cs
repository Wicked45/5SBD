using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Domain.Repositories;

public interface IOnibusRepository
{
    Task<Onibus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Onibus>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Onibus onibus, CancellationToken cancellationToken = default);
    Task UpdateAsync(Onibus onibus, CancellationToken cancellationToken = default);
}

