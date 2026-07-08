namespace SistemaViacao.Data;

using Microsoft.EntityFrameworkCore;
using SistemaViacao.Core;
using SistemaViacao.Core.Interfaces;


public class FrotaRepository : IFrotaRepository
{
    private readonly AppDbContext _context;

    public FrotaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Motorista?> ObterMotoristaPorId(int id)
    {
        return await _context.Motoristas
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Onibus?> ObterOnibusPorPlaca(string placa)
    {
        return await _context.Onibus
            .FirstOrDefaultAsync(o => o.Placa == placa);
    }

    public async Task<List<Onibus>> ListarFrota()
    {
        return await _context.Onibus
            .ToListAsync();
    }

    public async Task<List<Motorista>> ListarMotoristasAptos()
    {
        return await _context.Motoristas
            .Where(m => m.EmDescansoObrigatorio == false)
            .ToListAsync();
    }

    public async Task SalvarAlteracoes()
    {
        await _context.SaveChangesAsync();
    }
}

