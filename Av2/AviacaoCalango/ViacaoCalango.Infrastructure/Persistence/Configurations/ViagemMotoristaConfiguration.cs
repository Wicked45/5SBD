using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class ViagemMotoristaConfiguration : IEntityTypeConfiguration<ViagemMotorista>
{
    public void Configure(EntityTypeBuilder<ViagemMotorista> builder)
    {
        builder.ToTable("ViagemMotoristas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MotoristaId).IsRequired();
        builder.Property(x => x.ParadaOrigemId).IsRequired();
        builder.Property(x => x.ParadaDestinoId).IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => new { x.MotoristaId, x.ParadaOrigemId, x.ParadaDestinoId });
    }
}

