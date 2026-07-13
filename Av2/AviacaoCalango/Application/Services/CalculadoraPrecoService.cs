using AviacaoCalango.Domain.Entities;
using AviacaoCalango.Domain.Enums;

namespace AviacaoCalango.Application.Services;

public class CalculadoraPrecoService
{
    public decimal CalcularPrecoCumulativo(
        decimal precoBasePorKm,
        decimal distanciaKm,
        TipoOnibus tipoOnibus,
        decimal multiplicadorTipoOnibus,
        decimal descontoRotaCompletaPercent,
        bool rotaCompleta,
        decimal descontoAntecedenciaPercent,
        bool possuiDescontoAntecedencia)
    {
        if (precoBasePorKm <= 0) throw new ArgumentOutOfRangeException(nameof(precoBasePorKm));
        if (distanciaKm <= 0) throw new ArgumentOutOfRangeException(nameof(distanciaKm));
        if (multiplicadorTipoOnibus <= 0) throw new ArgumentOutOfRangeException(nameof(multiplicadorTipoOnibus));

        var preco = precoBasePorKm * distanciaKm;

        // regra: preço base por distância + multiplicador do tipo de ônibus
        preco *= multiplicadorTipoOnibus;

        // regra: desconto por rota completa (se for completa)
        if (rotaCompleta)
        {
            preco -= preco * (descontoRotaCompletaPercent / 100m);
        }

        // regra: desconto por antecedência parametrizável
        if (possuiDescontoAntecedencia)
        {
            preco -= preco * (descontoAntecedenciaPercent / 100m);
        }

        return Math.Round(preco, 2);
    }
}

