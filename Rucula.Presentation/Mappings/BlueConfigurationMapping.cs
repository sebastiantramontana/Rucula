using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class BlueConfigurationMapping(IHtmlSpanishNumberFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<BlueViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<BlueViewModel> modelMapper)
        => modelMapper
            .MapValue(b => formatter.Format(b.PurchasePrice)).ToElements.ById("blue-purchase").ToContent
            .MapValue(b => formatter.Format(b.SalePrice)).ToElements.ById("blue-sale").ToContent
            .Data;
}
