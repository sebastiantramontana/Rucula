﻿using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations
{
    internal class ChoicesService : IChoicesService
    {
        private readonly ITitulosService _titulosService;
        private readonly IDolarBlueProvider _dolarBlueProvider;
        private readonly IDolarCryptoProvider _dolarCryptoProvider;
        private readonly IWesternUnionProvider _westernUnionProvider;

        public ChoicesService(ITitulosService titulosService,
                              IDolarBlueProvider dolarBlueProvider,
                              IDolarCryptoProvider dolarCryptoProvider,
                              IWesternUnionProvider westernUnionProvider)
        {
            _titulosService = titulosService;
            _dolarBlueProvider = dolarBlueProvider;
            _dolarCryptoProvider = dolarCryptoProvider;
            _westernUnionProvider = westernUnionProvider;
        }

        public async Task<ChoicesInfo> GetChoices()
        {
            var dolarBlue = _dolarBlueProvider.GetCurrentBlue();
            var dolarCrypto = _dolarCryptoProvider.GetCurrentDolarCrypto();
            var dolarWesternUnion = _westernUnionProvider.GetCurrentDolarWesternUnion();

            await Task.WhenAll(dolarBlue, dolarCrypto, dolarWesternUnion);

            var rankingTitulos = await _titulosService.GetCclRankingTitulosIsin(dolarBlue.Result);

            var bestTitulo = rankingTitulos.FirstOrDefault();
            var winner = GetWinningChoice(bestTitulo, dolarCrypto.Result, dolarWesternUnion.Result);

            return new ChoicesInfo(winner ?? WinningChoice.NoWinners, rankingTitulos, dolarBlue.Result, dolarCrypto.Result, dolarWesternUnion.Result);
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
            => new WinningChoice(titulo.TituloPeso!.Simbolo, "No incluye comisiones ni costos de transferencias", titulo.CotizacionCcl!.Value);

        private static WinningChoice CreateWinner(DolarCrypto dolarCrypto)
            => new WinningChoice("Dolar Crypto", string.Empty, dolarCrypto.PrecioCompra);

        private static WinningChoice CreateWinner(DolarWesternUnion dolarWesternUnion)
            => new WinningChoice("Western Union", "No incluye costos ni fee", dolarWesternUnion.Price!.Value);
    }
}
