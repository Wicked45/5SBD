using AviacaoCalango.Domain.Enums;

namespace AviacaoCalango.Domain.Entities;

public class Passagem
{
    public Guid Id { get; private set; }

    public Guid ViagemId { get; private set; }
    public Guid OrigemParadaId { get; private set; }
    public Guid DestinoParadaId { get; private set; }

    // Identifica o assento alocado (1..capacidade).
    public int Assento { get; private set; }

    public Guid PassageiroId { get; private set; }

    public TipoPagamento TipoPagamento { get; private set; }
    public DateTimeOffset DataCompra { get; private set; }

    private Passagem() { }

    public Passagem(
        Guid id,
        Guid viagemId,
        Guid origemParadaId,
        Guid destinoParadaId,
        int assento,
        Guid passageiroId,
        TipoPagamento tipoPagamento,
        DateTimeOffset dataCompra)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (viagemId == Guid.Empty) throw new ArgumentException("viagemId inválido.", nameof(viagemId));
        if (origemParadaId == Guid.Empty) throw new ArgumentException("origemParadaId inválido.", nameof(origemParadaId));
        if (destinoParadaId == Guid.Empty) throw new ArgumentException("destinoParadaId inválido.", nameof(destinoParadaId));
        if (origemParadaId == destinoParadaId) throw new InvalidOperationException("origem e destino devem ser diferentes.");
        if (assento <= 0) throw new ArgumentOutOfRangeException(nameof(assento));
        if (passageiroId == Guid.Empty) throw new ArgumentException("passageiroId inválido.", nameof(passageiroId));
        if (dataCompra == default) throw new ArgumentException("dataCompra inválida.", nameof(dataCompra));

        Id = id;
        ViagemId = viagemId;
        OrigemParadaId = origemParadaId;
        DestinoParadaId = destinoParadaId;
        Assento = assento;
        PassageiroId = passageiroId;
        TipoPagamento = tipoPagamento;
        DataCompra = dataCompra;
    }
}

