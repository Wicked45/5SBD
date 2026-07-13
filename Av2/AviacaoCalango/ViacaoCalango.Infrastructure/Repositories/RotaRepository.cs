using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Repositories;

public class RotaRepository : IRotaRepository
{
    private readonly ViacaoCalangoDbContext _db;

    public RotaRepository(ViacaoCalangoDbContext db) => _db = db;

    public Task<Rota?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Rotas
            .AsNoTracking()
            .Include(r => (IReadOnlyCollection<RotaParada>)r.Paradas)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<IReadOnlyList<Rota>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.Rotas.AsNoTracking().ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Rota>>(t => t.Result, cancellationToken);

    public async Task AddAsync(Rota rota, CancellationToken cancellationToken = default)
    {
        _db.Rotas.Add(rota);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Rota rota, CancellationToken cancellationToken = default)
    {
        _db.Rotas.Update(rota);
        await _db.SaveChangesAsync(cancellationToken);
    }
}

