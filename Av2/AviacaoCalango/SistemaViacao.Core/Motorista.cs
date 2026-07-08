namespace SistemaViacao.Core;

public class Motorista
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int HorasTrabalhadas { get; private set; }
    public int KmRodados { get; private set; }
    public bool EmDescansoObrigatorio { get; private set; }

    private Motorista() { }

    public Motorista(int id, string nome, int horasTrabalhadas, int kmRodados, bool emDescansoObrigatorio)
    {
        Id = id;
        Nome = nome;
        HorasTrabalhadas = horasTrabalhadas;
        KmRodados = kmRodados;
        EmDescansoObrigatorio = emDescansoObrigatorio;
    }

    public Motorista(int id, string nome)
        : this(id, nome, 0, 0, false) { }

    public void RegistrarTurno(int horas, int km)
    {
        HorasTrabalhadas += horas;
        KmRodados += km;

        if (HorasTrabalhadas > 6 || KmRodados > 400)
            EmDescansoObrigatorio = true;
    }
}

