using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviacaoCalango.Infrastructure.Persistence;

public class ViacaoCalangoDbContext : DbContext
{
    public ViacaoCalangoDbContext(DbContextOptions<ViacaoCalangoDbContext> options) : base(options)
    {
    }

    public DbSet<Onibus> Onibus => Set<Onibus>();
    public DbSet<Parada> Paradas => Set<Parada>();
    public DbSet<Motorista> Motoristas => Set<Motorista>();

    public DbSet<Rota> Rotas => Set<Rota>();
    public DbSet<Viagem> Viagens => Set<Viagem>();
    public DbSet<Passagem> Passagens => Set<Passagem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ViacaoCalangoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

