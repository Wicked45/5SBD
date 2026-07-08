namespace SistemaViacao.Data;

using Microsoft.EntityFrameworkCore;
using SistemaViacao.Core;
using SistemaViacao.Core.Interfaces;

public class VendasRepository : IVendasRepository
{
    private readonly AppDbContext _context;

    public VendasRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SalvarPassagem(Passagem passagem)
    {
        _context.Passagens.Add(passagem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Rota>> ListarRotas()
    {
        return await _context.Rotas.ToListAsync();
    }
}

