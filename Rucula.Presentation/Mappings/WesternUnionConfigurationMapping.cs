using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class WesternUnionConfigurationMapping(IHtmlSpanishNumberFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<WesternUnionViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<WesternUnionViewModel> modelMapper)
        => modelMapper
            .MapValue(wu => formatter.Format(wu.NetPrice))
                .ToElements.ById("dolar-western-union").ToContent
                .ToElements.ById("net-dolar-western-union").ToContent
            .MapValue(wu => formatter.Format(wu.GrossPrice)).ToElements.ById("gross-dolar-western-union").ToContent
            .MapValue(wu => formatter.Format(wu.Fees)).ToElements.ById("gross-fees-western-union").ToContent
            .Data;
}
