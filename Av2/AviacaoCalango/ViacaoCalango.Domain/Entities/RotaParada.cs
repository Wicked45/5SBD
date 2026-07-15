namespace AviacaoCalango.Domain.Entities;

public class RotaParada
{
    public Guid Id { get; private set; }
    public Guid RotaId { get; private set; }

    public Guid ParadaId { get; private set; }
    public int Sequencia { get; private set; }

    private RotaParada() { }

    public RotaParada(Guid id, Guid paradaId, int sequencia)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (paradaId == Guid.Empty) throw new ArgumentException("ParadaId inválido.", nameof(paradaId));
        if (sequencia <= 0) throw new ArgumentOutOfRangeException(nameof(sequencia));

        Id = id;
        ParadaId = paradaId;
        Sequencia = sequencia;
    }
}

