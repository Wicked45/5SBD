using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class RotaParadaConfiguration : IEntityTypeConfiguration<RotaParada>
{
    public void Configure(EntityTypeBuilder<RotaParada> builder)
    {
        builder.ToTable("RotaParadas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Sequencia).IsRequired();
        builder.Property(x => x.RotaId).IsRequired();
        builder.Property(x => x.ParadaId).IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => new { x.RotaId, x.Sequencia }).IsUnique();
        builder.HasIndex(x => new { x.RotaId, x.ParadaId }).IsUnique();
    }
}

