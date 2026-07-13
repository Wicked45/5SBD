namespace AviacaoCalango.Domain.Entities;

public class Rota
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    private readonly List<RotaParada> _paradas = new();
    public IReadOnlyCollection<RotaParada> Paradas => _paradas;

    private Rota() { }

    public Rota(Guid id, string nome)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));

        Id = id;
        Nome = nome.Trim();
    }

    public void AdicionarParada(Guid paradaId, int sequencia)
    {
        if (paradaId == Guid.Empty) throw new ArgumentException("paradaId inválido.", nameof(paradaId));
        if (sequencia <= 0) throw new ArgumentOutOfRangeException(nameof(sequencia));

        if (_paradas.Any(p => p.ParadaId == paradaId))
            throw new InvalidOperationException("Parada já está na rota.");
        if (_paradas.Any(p => p.Sequencia == sequencia))
            throw new InvalidOperationException("Sequência já utilizada na rota.");

        _paradas.Add(new RotaParada(Guid.NewGuid(), paradaId, sequencia));
        _paradas.OrderBy(p => p.Sequencia).ToList();
    }
}

