using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class RotaConfiguration : IEntityTypeConfiguration<Rota>
{
    public void Configure(EntityTypeBuilder<Rota> builder)
    {
        builder.ToTable("Rotas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        // RotaParada é configurada como entidade própria (ver RotaParadaConfiguration se necessário).
        // Configura explicitamente UMA única navegação (Rota.Paradas -> RotaParada.RotaId),
        // evitando que o EF crie um segundo relacionamento "fantasma" via o campo _paradas.
        builder.HasMany(x => x.Paradas)
            .WithOne()
            .HasForeignKey(x => x.RotaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

