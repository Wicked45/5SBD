using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class MotoristaConfiguration : IEntityTypeConfiguration<Motorista>
{
    public void Configure(EntityTypeBuilder<Motorista> builder)
    {
        builder.ToTable("Motoristas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ParadaAtualId).IsRequired();

        builder.Property(x => x.KmDesdeUltimoDescanso).IsRequired();

        builder.Property(x => x.UltimoInicioDirecao);
        builder.Property(x => x.UltimoFimDescanso);

        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}

