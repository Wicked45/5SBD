namespace AV1Api.Domain.Entities;

public class Produto
{
    public string Sku { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public int EstoqueAtual { get; set; }
    public int QtdReposicao { get; set; }
}

