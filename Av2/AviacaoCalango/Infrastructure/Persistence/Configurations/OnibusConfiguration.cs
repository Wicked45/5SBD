using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class OnibusConfiguration : IEntityTypeConfiguration<Onibus>
{
    public void Configure(EntityTypeBuilder<Onibus> builder)
    {
        builder.ToTable("Onibus");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Tipo)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Capacidade).IsRequired();
        builder.Property(x => x.KmDesdeUltimaManutencao).IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Tipo).HasMaxLength(50);
    }
}

