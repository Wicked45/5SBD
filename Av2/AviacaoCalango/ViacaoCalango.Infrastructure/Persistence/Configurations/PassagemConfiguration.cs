using AviacaoCalango.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviacaoCalango.Infrastructure.Persistence.Configurations;

public class PassagemConfiguration : IEntityTypeConfiguration<Passagem>
{
    public void Configure(EntityTypeBuilder<Passagem> builder)
    {
        builder.ToTable("Passagens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ViagemId).IsRequired();
        builder.Property(x => x.OrigemParadaId).IsRequired();
        builder.Property(x => x.DestinoParadaId).IsRequired();
        builder.Property(x => x.Assento).IsRequired();
        builder.Property(x => x.PassageiroId).IsRequired();
        builder.Property(x => x.TipoPagamento).HasConversion<int>().IsRequired();
        builder.Property(x => x.DataCompra).IsRequired();

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => new { x.ViagemId, x.Assento });
    }
}

