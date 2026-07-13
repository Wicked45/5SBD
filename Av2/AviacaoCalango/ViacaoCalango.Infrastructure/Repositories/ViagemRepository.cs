using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Repositories;

public class ViagemRepository : IViagemRepository
{
    private readonly ViacaoCalangoDbContext _db;

    public ViagemRepository(ViacaoCalangoDbContext db) => _db = db;

    public Task<Viagem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Viagens.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<IReadOnlyList<Viagem>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.Viagens.AsNoTracking().ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Viagem>>(t => t.Result, cancellationToken);

    public async Task AddAsync(Viagem viagem, CancellationToken cancellationToken = default)
    {
        _db.Viagens.Add(viagem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Viagem viagem, CancellationToken cancellationToken = default)
    {
        _db.Viagens.Update(viagem);
        await _db.SaveChangesAsync(cancellationToken);
    }
}

