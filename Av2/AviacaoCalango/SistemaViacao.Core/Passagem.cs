namespace SistemaViacao.Core;

public class Passagem
{
    public int Id { get; private set; }
    public int RotaId { get; private set; }
    public string NomePassageiro { get; private set; } = string.Empty;
    public DateTime DataViagem { get; private set; }

    // "Cartao" ou "Dinheiro"
    public string TipoPagamento { get; private set; } = string.Empty;

    public decimal ValorFinal { get; private set; }

    private Passagem() { }

    public Passagem(int id, int rotaId, string nomePassageiro, DateTime dataViagem, string tipoPagamento)
    {
        Id = id;
        RotaId = rotaId;
        NomePassageiro = nomePassageiro;
        DataViagem = dataViagem;
        TipoPagamento = tipoPagamento;
        ValorFinal = 0m;
    }

    public void CalcularValor(Onibus onibus, int diasAntecedencia, Rota rota)
    {
        decimal valor = rota.PrecoBase;

        if (diasAntecedencia > 30)
            valor *= 0.8m; // 20% de desconto

        if (string.Equals(onibus.Tipo, "Leito", StringComparison.OrdinalIgnoreCase))
            valor *= 1.5m; // 50% de acréscimo

        ValorFinal = valor;
    }
}

