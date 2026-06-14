using AV1Api.Domain;
using AV1Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AV1Api.Infrastructure;

public class BazarRepository : IBazarRepository
{
    private readonly BazarDbContext _context;

    public BazarRepository(BazarDbContext context)
    {
        _context = context;
    }

    public Task<List<Pedido>> ObterPedidosAsync(CancellationToken cancellationToken = default)
    {
        return _context.Pedidos
            .OrderByDescending(p => p.ValorTotal)
            .ToListAsync(cancellationToken);
    }

    public Task<List<CompraReposicao>> ObterItensReposicaoAsync(CancellationToken cancellationToken = default)
    {
        // Mantém todos os itens; você pode filtrar por status se desejar.
        return _context.ComprasReposicao.ToListAsync(cancellationToken);
    }

    public Task ExecutarImportarEtlAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.ExecuteSqlRawAsync("CALL prc_etl_carga_pedidos();", cancellationToken);
    }

    public Task ExecutarProcessarEstoqueAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.ExecuteSqlRawAsync("CALL prc_processar_estoque_pedidos();", cancellationToken);
    }
}

