using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Domain.Repositories;

public interface IPassagemRepository
{
    Task<Passagem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Passagem>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Passagem passagem, CancellationToken cancellationToken = default);
    Task UpdateAsync(Passagem passagem, CancellationToken cancellationToken = default);
}

