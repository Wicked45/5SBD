using AviacaoCalango.Domain.Entities;

namespace AviacaoCalango.Application.Services;

public class VendaAppService
{
    /// <summary>
    /// Realiza a compra e aloca o assento minimizando espaços vazios.
    /// Observação: nesta fase o domínio ainda está parcial; a regra de assentos será refinada
    /// assim que as entidades de viagem/passagem forem criadas.
    /// </summary>
    public (int assentoAlocado, bool assentoEscolhido) Comprar(
        int capacidadeOnibus,
        IReadOnlyCollection<int> assentosOcupados,
        int? assentoEscolhido)
    {
        if (capacidadeOnibus <= 0) throw new ArgumentOutOfRangeException(nameof(capacidadeOnibus));
        if (assentosOcupados is null) throw new ArgumentNullException(nameof(assentosOcupados));

        // Se usuário escolheu um assento, valida disponibilidade.
        if (assentoEscolhido.HasValue)
        {
            var assento = assentoEscolhido.Value;
            if (assento < 1 || assento > capacidadeOnibus)
                throw new ArgumentOutOfRangeException(nameof(assentoEscolhido), "Assento fora da capacidade.");

            if (assentosOcupados.Contains(assento))
                throw new InvalidOperationException("Assento escolhido já está ocupado.");

            return (assento, true);
        }

        // Minimizar espaços vazios: estratégia simples que tende a agrupar passageiros.
        // Busca intervalos contíguos de assentos livres com menor “buraco” ao redor.
        var ocupados = assentosOcupados.OrderBy(x => x).ToArray();
        var ocupSet = assentosOcupados.ToHashSet();

        // Tenta primeiro assentos internos livres adjacentes a ocupados (para reduzir vazios entre lotes).
        for (int i = 1; i <= capacidadeOnibus; i++)
        {
            if (ocupSet.Contains(i)) continue;

            bool vizEsqOcup = i > 1 && ocupSet.Contains(i - 1);
            bool vizDirOcup = i < capacidadeOnibus && ocupSet.Contains(i + 1);

            if (vizEsqOcup || vizDirOcup)
                return (i, false);
        }

        // Fallback: escolhe o primeiro assento livre.
        for (int i = 1; i <= capacidadeOnibus; i++)
        {
            if (!ocupSet.Contains(i))
                return (i, false);
        }

        throw new InvalidOperationException("Não há assentos disponíveis.");
    }
}

