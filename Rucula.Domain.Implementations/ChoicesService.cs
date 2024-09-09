using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations
{
    internal class ChoicesService : IChoicesService
    {
        private readonly ITitulosService _titulosService;
        private readonly IDolarBlueProvider _dolarBlueProvider;
        private readonly IDolarCryptoProvider _dolarCryptoProvider;
        private readonly IWesternUnionProvider _westernUnionProvider;
        private readonly IDolarDiarcoProvider _dolarDiarcoProvider;

        public ChoicesService(ITitulosService titulosService,
                              IDolarBlueProvider dolarBlueProvider,
                              IDolarCryptoProvider dolarCryptoProvider,
                              IWesternUnionProvider westernUnionProvider,
                              IDolarDiarcoProvider dolarDiarcoProvider)
        {
            _titulosService = titulosService;
            _dolarBlueProvider = dolarBlueProvider;
            _dolarCryptoProvider = dolarCryptoProvider;
            _westernUnionProvider = westernUnionProvider;
            _dolarDiarcoProvider = dolarDiarcoProvider;
        }

        public async Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions)
        {
            var dolarBlue = _dolarBlueProvider.GetCurrentBlue();
            var dolarCrypto = _dolarCryptoProvider.GetCurrentDolarCrypto();
            var dolarWesternUnion = _westernUnionProvider.GetCurrentDolarWesternUnion();
            var dolarDiarco = _dolarDiarcoProvider.GetCurrentDolarDiarco();

            await Task.WhenAll(dolarBlue, dolarCrypto, dolarWesternUnion, dolarDiarco).ConfigureAwait(false);

            var rankingTitulos = await _titulosService.GetNetCclRanking(dolarBlue.Result, bondCommissions).ConfigureAwait(false);

            return CreateWinningChoice(rankingTitulos, dolarBlue.Result, dolarCrypto.Result, dolarWesternUnion.Result, dolarDiarco.Result);
        }

        public ChoicesInfo RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions)
        {
            var rankingTitulos = _titulosService.RecalculateNetCclRanking(choices.RankingTitulos, bondCommissions);
            return CreateWinningChoice(rankingTitulos, choices.Blue, choices.DolarCrypto, choices.DolarWesternUnion, choices.DolarDiarco);
        }

        private static ChoicesInfo CreateWinningChoice(IEnumerable<TituloIsin> rankingTitulos, Blue dolarBlue, DolarCrypto dolarCrypto, DolarWesternUnion dolarWesternUnion, DolarDiarco dolarDiarco)
        {
            var bestTitulo = rankingTitulos.FirstOrDefault();
            var winner = GetWinningChoice(bestTitulo, dolarCrypto, dolarWesternUnion);

            return new ChoicesInfo(winner ?? WinningChoice.NoWinners, rankingTitulos, dolarBlue, dolarCrypto, dolarWesternUnion, dolarDiarco);
        }

        private static WinningChoice? GetWinningChoice(TituloIsin? titulo, DolarCrypto dolarCrypto, DolarWesternUnion dolarWesternUnion)
        {
            var winners = new List<WinningChoice>(3);

            if (titulo is not null)
                winners.Add(CreateWinner(titulo));

            winners.Add(CreateWinner(dolarCrypto));

            if (dolarWesternUnion.Price is not null)
                winners.Add(CreateWinner(dolarWesternUnion));

            var winner = winners
                        .OrderByDescending(w => w.DolarPrice)
                        .FirstOrDefault();

            return winner;
        }

        private static WinningChoice CreateWinner(TituloIsin titulo)
            => new(titulo.TituloPeso!.Simbolo, "Incluye comisiones, pero no costos de transferencias", titulo.NetCcl!.Value);

        private static WinningChoice CreateWinner(DolarCrypto dolarCrypto)
            => new("Dolar Crypto", string.Empty, dolarCrypto.PrecioCompra);

        private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
            => new("Western Union", "No incluye costos ni fee", dolarWesternUnion.Price!.Value);
    }
}
