using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Repositories;

public class OnibusRepository : IOnibusRepository
{
    private readonly ViacaoCalangoDbContext _db;

    public OnibusRepository(ViacaoCalangoDbContext db) => _db = db;

    public Task<Onibus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Onibus.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<IReadOnlyList<Onibus>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.Onibus.AsNoTracking().ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Onibus>>(t => t.Result, cancellationToken);

    public async Task AddAsync(Onibus onibus, CancellationToken cancellationToken = default)
    {
        _db.Onibus.Add(onibus);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Onibus onibus, CancellationToken cancellationToken = default)
    {
        _db.Onibus.Update(onibus);
        await _db.SaveChangesAsync(cancellationToken);
    }
}

