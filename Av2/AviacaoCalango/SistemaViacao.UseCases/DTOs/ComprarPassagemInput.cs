namespace SistemaViacao.UseCases.DTOs;

public record ComprarPassagemInput(
    int Id,
    int RotaId,
    string NomePassageiro,
    DateTime DataViagem,
    string TipoPagamento,
    string PlacaOnibus,
    int DiasAntecedencia
);

