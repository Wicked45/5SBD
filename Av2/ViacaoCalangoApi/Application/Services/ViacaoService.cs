using Microsoft.EntityFrameworkCore;
using ViacaoCalangoApi.Domain.Entities;
using ViacaoCalangoApi.Infrastructure.Data;

namespace ViacaoCalangoApi.Application.Services;

public class ViacaoService
{
    private readonly AppDbContext _context;

    public ViacaoService(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Motorista>> ListarMotoristasAptosAsync()
    {
        return _context.Motoristas
            .Where(m => !m.EmDescansoObrigatorio)
            .ToListAsync();
    }

    public Task<List<Onibus>> ListarFrotaAsync()
    {
        return _context.Onibus.ToListAsync();
    }

    public async Task RegistrarFimDeViagemAsync(int motoristaId, string placaOnibus, int horasViajadas, int kmPercorridos)
    {
        var motorista = await _context.Motoristas
            .FirstOrDefaultAsync(m => m.Id == motoristaId);

        if (motorista is null)
            throw new InvalidOperationException($"Motorista com id {motoristaId} não encontrado.");

        var onibus = await _context.Onibus
            .FirstOrDefaultAsync(o => o.Placa == placaOnibus);

        if (onibus is null)
            throw new InvalidOperationException($"Ônibus com placa {placaOnibus} não encontrado.");

        motorista.RegistrarTurno(horasViajadas, kmPercorridos);
        onibus.RegistrarKmViagem(kmPercorridos);

        await _context.SaveChangesAsync();
    }
}

