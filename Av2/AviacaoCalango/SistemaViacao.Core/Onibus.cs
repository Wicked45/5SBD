namespace SistemaViacao.Core;

public class Onibus
{
    public string Placa { get; private set; } = string.Empty;
    public int Capacidade { get; private set; }
    public string Tipo { get; private set; } = string.Empty;
    public int KmRodados { get; private set; }
    public bool PrecisaRevisao { get; private set; }

    private Onibus() { }

    public Onibus(string placa, int capacidade, string tipo, int kmRodados, bool precisaRevisao)
    {
        Placa = placa;
        Capacidade = capacidade;
        Tipo = tipo;
        KmRodados = kmRodados;
        PrecisaRevisao = precisaRevisao;
    }

    public Onibus(string placa, int capacidade, string tipo)
        : this(placa, capacidade, tipo, 0, false) { }

    public void RegistrarKmViagem(int km)
    {
        KmRodados += km;

        if (KmRodados > 10000)
            PrecisaRevisao = true;
    }
}

