using AviacaoCalango.Domain.Enums;

namespace AviacaoCalango.Domain.Entities;

public class Motorista
{
    private const int LimiteHorasDirecao = 6;
    private const int LimiteKmDirecao = 400;
    private const int LimiteDescansoHoras = 12;

    public Guid Id { get; private set; }
    public StatusMotorista Status { get; private set; }

    /// <summary>
    /// Indica a parada atual em que o motorista se encontra.
    /// </summary>
    public Guid ParadaAtualId { get; private set; }

    public DateTimeOffset? UltimoInicioDirecao { get; private set; }
    public int KmDesdeUltimoDescanso { get; private set; }
    public DateTimeOffset? UltimoFimDescanso { get; private set; }

    private Motorista() { }

    public Motorista(Guid id, Guid paradaAtualId)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (paradaAtualId == Guid.Empty) throw new ArgumentException("ParadaAtualId inválido.", nameof(paradaAtualId));

        Id = id;
        ParadaAtualId = paradaAtualId;
        Status = StatusMotorista.Ativo;
        KmDesdeUltimoDescanso = 0;
        UltimoInicioDirecao = null;
        UltimoFimDescanso = null;
    }

    public void AlocarParaParada(Guid paradaId)
    {
        if (paradaId == Guid.Empty) throw new ArgumentException("paradaId inválido.", nameof(paradaId));
        ParadaAtualId = paradaId;
    }

    /// <summary>
    /// Valida a regra de escalonamento do motorista antes de iniciar uma nova direção.
    /// Deve garantir: após dirigir até 6h ou 400km, precisa de descanso mínimo de 12h.
    /// </summary>
    public void ValidarDisponibilidade(DateTimeOffset agora, TimeSpan tempoDirecaoPlanejado, int kmPlanejados)
    {
        if (tempoDirecaoPlanejado.TotalHours < 0)
            throw new ArgumentOutOfRangeException(nameof(tempoDirecaoPlanejado));
        if (kmPlanejados < 0)
            throw new ArgumentOutOfRangeException(nameof(kmPlanejados));

        // Atualiza contexto mínimo (modelo simples para validação)
        if (UltimoInicioDirecao is null)
        {
            UltimoInicioDirecao = agora;
        }

        bool ultrapassaLimites =
            tempoDirecaoPlanejado.TotalHours >= LimiteHorasDirecao ||
            kmPlanejados >= LimiteKmDirecao;

        if (!ultrapassaLimites)
            return;

        if (UltimoFimDescanso is null)
            throw new InvalidOperationException("Motorista sem registro de descanso anterior; necessário descanso mínimo.");

        var horasDescanso = (agora - UltimoFimDescanso.Value).TotalHours;
        if (horasDescanso < LimiteDescansoHoras)
            throw new InvalidOperationException("Motorista não possui 12h mínimas de descanso.");
    }

    public void RegistrarFimDescanso(DateTimeOffset fim)
    {
        UltimoFimDescanso = fim;
        KmDesdeUltimoDescanso = 0;
        Status = StatusMotorista.Ativo;
    }

    public void RegistrarDirecao(DateTimeOffset inicio, DateTimeOffset fim, int kmPercorridos)
    {
        if (fim <= inicio) throw new ArgumentException("fim deve ser maior que inicio.");
        if (kmPercorridos <= 0) throw new ArgumentOutOfRangeException(nameof(kmPercorridos));

        UltimoInicioDirecao = inicio;
        KmDesdeUltimoDescanso += kmPercorridos;

        bool excedeu = (fim - inicio).TotalHours >= LimiteHorasDirecao || KmDesdeUltimoDescanso >= LimiteKmDirecao;
        if (excedeu)
            Status = StatusMotorista.EmDescanso;
    }

    public void EmitirAvisoParaViagem()
    {
        if (Status == StatusMotorista.Inativo)
            throw new InvalidOperationException("Motorista inativo não pode ser notificado.");

        Status = StatusMotorista.NotificadoParaViagem;
    }

    public void Inativar()
    {
        Status = StatusMotorista.Inativo;
    }
}

