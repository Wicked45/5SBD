namespace AV1Api.Domain.Entities;

public class CompraReposicao
{
    public int IdCompra { get; set; }
    public string Sku { get; set; } = null!;
    public int QuantidadeAComprar { get; set; }
    public string OrderIdPendente { get; set; } = null!;
    public string Status { get; set; } = null!;
}

