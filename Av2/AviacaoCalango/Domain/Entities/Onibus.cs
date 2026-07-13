using AviacaoCalango.Domain.Enums;

namespace AviacaoCalango.Domain.Entities;

public class Onibus
{
    private const int KmManutencaoObrigatoria = 10_000;

    public Guid Id { get; private set; }
    public TipoOnibus Tipo { get; private set; }
    public StatusOnibus Status { get; private set; }
    public int Capacidade { get; private set; }

    /// <summary>
    /// Quilometragem acumulada desde a última manutenção.
    /// </summary>
    public int KmDesdeUltimaManutencao { get; private set; }

    private Onibus() { }

    public Onibus(Guid id, TipoOnibus tipo, int capacidade)
    {
        if (capacidade != 23 && capacidade != 28 && capacidade != 32)
            throw new ArgumentException("A capacidade do ônibus deve ser 23, 28 ou 32.", nameof(capacidade));

        Id = id == Guid.Empty ? throw new ArgumentException("Id inválido.", nameof(id)) : id;
        Tipo = tipo;
        Capacidade = capacidade;
        Status = StatusOnibus.Disponivel;
        KmDesdeUltimaManutencao = 0;
    }

    public void RegistrarKm(int km)
    {
        if (km <= 0) throw new ArgumentOutOfRangeException(nameof(km), "Km deve ser maior que zero.");

        KmDesdeUltimaManutencao += km;

        if (KmDesdeUltimaManutencao >= KmManutencaoObrigatoria)
            Status = StatusOnibus.EmManutencao;
    }

    public void RealizarManutencao()
    {
        KmDesdeUltimaManutencao = 0;
        Status = StatusOnibus.Disponivel;
    }

    public void Inativar()
    {
        Status = StatusOnibus.Inativo;
    }
}

