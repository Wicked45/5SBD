using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Repositories;

public class PassagemRepository : IPassagemRepository
{
    private readonly ViacaoCalangoDbContext _db;

    public PassagemRepository(ViacaoCalangoDbContext db) => _db = db;

    public Task<Passagem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Passagens.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<IReadOnlyList<Passagem>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.Passagens.AsNoTracking().ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Passagem>>(t => t.Result, cancellationToken);

    public async Task AddAsync(Passagem passagem, CancellationToken cancellationToken = default)
    {
        _db.Passagens.Add(passagem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Passagem passagem, CancellationToken cancellationToken = default)
    {
        _db.Passagens.Update(passagem);
        await _db.SaveChangesAsync(cancellationToken);
    }
}

