using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class ViagemConfiguration : IEntityTypeConfiguration<Viagem>
{
    public void Configure(EntityTypeBuilder<Viagem> builder)
    {
        builder.ToTable("Viagens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RotaId).IsRequired();
        builder.Property(x => x.DataHoraPartida).IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        // Motoristas e passagens são entidades próprias (ViagemMotorista/Passagem) configuradas separadamente.
    }
}

