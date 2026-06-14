namespace AV1Api.Domain.Entities;

public class Pedido
{
    public string OrderId { get; set; } = null!;
    public string CpfCliente { get; set; } = null!;
    public DateTime DataCompra { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = null!;
}

