namespace AviacaoCalango.Domain.Entities;

public class Viagem
{
    public Guid Id { get; private set; }
    public Guid RotaId { get; private set; }
    public DateTimeOffset DataHoraPartida { get; private set; }

    private readonly List<ViagemMotorista> _motoristas = new();
    public IReadOnlyCollection<ViagemMotorista> Motoristas => _motoristas;

    private readonly List<Passagem> _passagens = new();
    public IReadOnlyCollection<Passagem> Passagens => _passagens;

    private Viagem() { }

    public Viagem(Guid id, Guid rotaId, DateTimeOffset dataHoraPartida)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (rotaId == Guid.Empty) throw new ArgumentException("rotaId inválido.", nameof(rotaId));
        if (dataHoraPartida <= DateTimeOffset.MinValue) throw new ArgumentException("DataHoraPartida inválida.", nameof(dataHoraPartida));


        Id = id;
        RotaId = rotaId;
        DataHoraPartida = dataHoraPartida;
    }

    public void RegistrarMotoristaTrecho(Guid motoristaId, Guid paradaOrigemId, Guid paradaDestinoId)
    {
        if (motoristaId == Guid.Empty) throw new ArgumentException("motoristaId inválido.", nameof(motoristaId));
        if (paradaOrigemId == Guid.Empty) throw new ArgumentException("paradaOrigemId inválido.", nameof(paradaOrigemId));
        if (paradaDestinoId == Guid.Empty) throw new ArgumentException("paradaDestinoId inválido.", nameof(paradaDestinoId));

        if (paradaOrigemId == paradaDestinoId)
            throw new InvalidOperationException("paradaOrigemId e paradaDestinoId devem ser diferentes.");

        _motoristas.Add(new ViagemMotorista(Guid.NewGuid(), motoristaId, paradaOrigemId, paradaDestinoId));
    }

    public void RegistrarPassagem(Passagem passagem)
    {
        if (passagem is null) throw new ArgumentNullException(nameof(passagem));
        if (passagem.ViagemId != Id) throw new InvalidOperationException("A passagem não pertence a esta viagem.");

        _passagens.Add(passagem);
    }
}

