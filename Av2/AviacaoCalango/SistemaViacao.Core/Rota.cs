namespace SistemaViacao.Core;

public class Rota
{
    public int Id { get; private set; }
    public string Origem { get; private set; } = string.Empty;
    public string Destino { get; private set; } = string.Empty;
    public decimal PrecoBase { get; private set; }

    private Rota() { }

    public Rota(int id, string origem, string destino, decimal precoBase)
    {
        Id = id;
        Origem = origem;
        Destino = destino;
        PrecoBase = precoBase;
    }
}

