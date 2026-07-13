using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Domain.Repositories;

public interface IViagemRepository
{
    Task<Viagem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Viagem>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Viagem viagem, CancellationToken cancellationToken = default);
    Task UpdateAsync(Viagem viagem, CancellationToken cancellationToken = default);
}

