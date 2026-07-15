namespace AviacaoCalango.Domain.Entities;

public class Parada
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    private Parada() { }

    public Parada(Guid id, string nome)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(id));
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));

        Id = id;
        Nome = nome.Trim();
    }
}

