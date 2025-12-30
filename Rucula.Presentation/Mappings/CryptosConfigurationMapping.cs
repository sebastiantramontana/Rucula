using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class CryptosConfigurationMapping(ISpanishPriceFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<CryptosViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<CryptosViewModel> modelMapper)
        => modelMapper
            .MapCollection(c => c.Exchanges)
                .ToContainerElements.ById("crypto-table-container")
                    .FromTemplate("crypto-template")
                    .MapValue(e => e.ExchangeName).ToElements.ByQuery("[data-crypto-id='exchange-name']").ToContent
                    .MapValue(e => formatter.Format(e.GrossUsdc)).ToElements.ByQuery("[data-crypto-id='usdc-gross']").ToContent
                    .MapValue(e => formatter.Format(e.GrossUsdt)).ToElements.ByQuery("[data-crypto-id='usdt-gross']").ToContent
                    .MapValue(e => formatter.Format(e.GrossDai)).ToElements.ByQuery("[data-crypto-id='dai-gross']").ToContent
                    .MapCollection(e => e.Blockchains)
                        .ToTables.ByQuery("[data-crypto-id='crypto-table']")
                        .PopulatingRows.ToTBody(2)
                            .FromTemplate("crypto-blockchain-template")
                            .MapValue(b => b.BlockchainName).ToElements.ByQuery("[data-crypto-id='blockchain-name']").ToContent
                            .MapValue(b => formatter.Format(b.UsdcFee)).ToElements.ByQuery("[data-crypto-id='usdc-fee']").ToContent
                            .MapValue(b => formatter.Format(b.NetUsdc)).ToElements.ByQuery("[data-crypto-id='usdc-net']").ToContent
                            .MapValue(b => formatter.Format(b.UsdtFee)).ToElements.ByQuery("[data-crypto-id='usdt-fee']").ToContent
                            .MapValue(b => formatter.Format(b.NetUsdt)).ToElements.ByQuery("[data-crypto-id='usdt-net']").ToContent
                            .MapValue(b => formatter.Format(b.DaiFee)).ToElements.ByQuery("[data-crypto-id='dai-fee']").ToContent
                            .MapValue(b => formatter.Format(b.NetDai)).ToElements.ByQuery("[data-crypto-id='dai-net']").ToContent
                    .EndCollection
            .EndCollection
            .Data;
}