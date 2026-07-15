using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class ParadaConfiguration : IEntityTypeConfiguration<Parada>
{
    public void Configure(EntityTypeBuilder<Parada> builder)
    {
        builder.ToTable("Paradas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}

