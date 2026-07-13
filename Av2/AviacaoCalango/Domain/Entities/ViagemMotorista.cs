namespace AviacaoCalango.Domain.Entities;

public class ViagemMotorista
{
    public Guid Id { get; private set; }
    public Guid MotoristaId { get; private set; }

    public Guid ParadaOrigemId { get; private set; }
    public Guid ParadaDestinoId { get; private set; }

    private ViagemMotorista() { }

    public ViagemMotorista(Guid id, Guid motoristaId, Guid paradaOrigemId, Guid paradaDestinoId)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (motoristaId == Guid.Empty) throw new ArgumentException("motoristaId inválido.", nameof(motoristaId));
        if (paradaOrigemId == Guid.Empty) throw new ArgumentException("paradaOrigemId inválido.", nameof(paradaOrigemId));
        if (paradaDestinoId == Guid.Empty) throw new ArgumentException("paradaDestinoId inválido.", nameof(paradaDestinoId));
        if (paradaOrigemId == paradaDestinoId) throw new InvalidOperationException("Origem e destino devem ser diferentes.");

        Id = id;
        MotoristaId = motoristaId;
        ParadaOrigemId = paradaOrigemId;
        ParadaDestinoId = paradaDestinoId;
    }
}

