using Microsoft.EntityFrameworkCore;
using ViacaoCalangoApi.Domain.Entities;

namespace ViacaoCalangoApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Onibus> Onibus { get; set; }
    public DbSet<Motorista> Motoristas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Onibus>().HasKey(o => o.Placa);
        modelBuilder.Entity<Motorista>().HasKey(m => m.Id);

        // Dados de Teste
        modelBuilder.Entity<Motorista>().HasData(
            new Motorista(1, "João Silva", 2, 0, false),
            new Motorista(2, "Maria Souza", 3, 50, false)
        );

        modelBuilder.Entity<Onibus>().HasData(
            new Onibus("ABC-1234", 40, "Urbano", 0, false),
            new Onibus("DEF-5678", 50, "Intermunicipal", 12000, true)
        );
    }
}