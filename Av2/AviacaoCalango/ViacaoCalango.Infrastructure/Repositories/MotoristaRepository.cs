using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Enums;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Repositories;

public class MotoristaRepository : IMotoristaRepository
{
    private readonly ViacaoCalangoDbContext _db;

    public MotoristaRepository(ViacaoCalangoDbContext db) => _db = db;

    public Task<Motorista?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Motoristas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<IReadOnlyList<Motorista>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.Motoristas.AsNoTracking().ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Motorista>>(t => t.Result, cancellationToken);

    public Task<IReadOnlyList<Motorista>> ListDisponiveisNaParadaAsync(Guid paradaId, DateTimeOffset agora, CancellationToken cancellationToken = default)
    {
        // Regra simplificada para início: status apto e na estação.
        // As validações 6h/400km e 12h serão aplicadas no AppService via entidade/métodos.
        return _db.Motoristas.AsNoTracking()
            .Where(m => m.ParadaAtualId == paradaId)
            .Where(m => m.Status != StatusMotorista.Inativo)
            .ToListAsync(cancellationToken)
            .ContinueWith<IReadOnlyList<Motorista>>(t => t.Result, cancellationToken);
    }

    public async Task AddAsync(Motorista motorista, CancellationToken cancellationToken = default)
    {
        _db.Motoristas.Add(motorista);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Motorista motorista, CancellationToken cancellationToken = default)
    {
        _db.Motoristas.Update(motorista);
        await _db.SaveChangesAsync(cancellationToken);
    }
}

