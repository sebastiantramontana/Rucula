using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class DolarAppConfigurationMapping(IHtmlSpanishNumberFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<DolarAppViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<DolarAppViewModel> modelMapper)
        => modelMapper
            .MapValue(wu => formatter.Format(wu.NetPrice))
                .ToElements.ById("dolarapp-price").ToContent
                .ToElements.ById("net-price-dolarapp").ToContent
            .MapValue(wu => formatter.Format(wu.GrossPrice)).ToElements.ById("gross-price-dolarapp").ToContent
            .MapValue(wu => formatter.Format(wu.FixedFee)).ToElements.ById("gross-fees-dolarapp").ToContent
            .Data;
}
