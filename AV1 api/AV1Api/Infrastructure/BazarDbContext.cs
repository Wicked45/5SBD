using AV1Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AV1Api.Infrastructure;

public class BazarDbContext : DbContext
{
    public BazarDbContext(DbContextOptions<BazarDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<CompraReposicao> ComprasReposicao => Set<CompraReposicao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e =>
        {
            e.ToTable("clientes");
            e.HasKey(x => x.Cpf);
            e.Property(x => x.Cpf).HasColumnName("cpf");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("produtos");
            e.HasKey(x => x.Sku);
            e.Property(x => x.Sku).HasColumnName("sku");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.EstoqueAtual).HasColumnName("estoque_atual");
            e.Property(x => x.QtdReposicao).HasColumnName("qtd_reposicao");
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("pedidos");
            e.HasKey(x => x.OrderId);
            e.Property(x => x.OrderId).HasColumnName("order_id");
            e.Property(x => x.CpfCliente).HasColumnName("cpf_cliente");
            e.Property(x => x.DataCompra).HasColumnName("data_compra");
            e.Property(x => x.ValorTotal).HasColumnName("valor_total");
            e.Property(x => x.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CompraReposicao>(e =>
        {
            e.ToTable("comprasreposicao");
            e.HasKey(x => x.IdCompra);
            e.Property(x => x.IdCompra).HasColumnName("id_compra");
            e.Property(x => x.Sku).HasColumnName("sku");
            e.Property(x => x.QuantidadeAComprar).HasColumnName("quantidade_a_comprar");
            e.Property(x => x.OrderIdPendente).HasColumnName("order_id_pendente");
            e.Property(x => x.Status).HasColumnName("status");
        });
    }
}

