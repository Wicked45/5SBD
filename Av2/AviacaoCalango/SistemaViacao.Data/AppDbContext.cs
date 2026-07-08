namespace SistemaViacao.Data;

using Microsoft.EntityFrameworkCore;
using SistemaViacao.Core;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Motorista> Motoristas => Set<Motorista>();
    public DbSet<Onibus> Onibus => Set<Onibus>();
    public DbSet<Rota> Rotas => Set<Rota>();
    public DbSet<Passagem> Passagens => Set<Passagem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Motorista>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Onibus>()
            .HasKey(o => o.Placa);

        modelBuilder.Entity<Motorista>().HasData(
            new Motorista(1, "Joao")
        );

        modelBuilder.Entity<Onibus>().HasData(
            new Onibus("ABC-1234", 40, "Urbano")
        );

        modelBuilder.Entity<Rota>()
            .HasData(
                new Rota(1, "SP", "RJ", 100m)
            );

        modelBuilder.Entity<Passagem>();

    }
}

