using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class BondsConfigurationMapping(ISpanishPriceFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<BondsViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<BondsViewModel> modelMapper)
        => modelMapper
            .MapCollection(b => b.Bonds)
                .ToTables.ById("bonds-table")
                    .PopulatingRows.FromTemplate("bonds-table-row-template")
                    .MapValue(r => r.ArLabel).ToElements.ByQuery("[data-bonds-id='symbol-pesos']").ToContent
                    .MapValue(r => formatter.Format(r.ArPrice)).ToElements.ByQuery("[data-bonds-id='price-pesos']").ToContent
                    .MapValue(r => r.CableLabel).ToElements.ByQuery("[data-bonds-id='symbol-cable']").ToContent
                    .MapValue(r => formatter.Format(r.CablePrice)).ToElements.ByQuery("[data-bonds-id='price-cable']").ToContent
                    .MapValue(r => r.MepLabel).ToElements.ByQuery("[data-bonds-id='symbol-mep']").ToContent
                    .MapValue(r => formatter.Format(r.MepPrice)).ToElements.ByQuery("[data-bonds-id='price-mep']").ToContent
                    .MapValue(r => formatter.Format(r.GrossCcl)).ToElements.ByQuery("[data-bonds-id='gross-ccl']").ToContent
                    .MapValue(r => formatter.Format(r.NetCcl)).ToElements.ByQuery("[data-bonds-id='net-ccl']").ToContent
                    .MapValue(r => formatter.Format(r.MepOverCable)).ToElements.ByQuery("[data-bonds-id='mep-over-cable']").ToContent
                    .MapValue(r => formatter.Format(r.BlueOverCcl)).ToElements.ByQuery("[data-bonds-id='blue-over-ccl']").ToContent
            .EndCollection
            .Data;
}
