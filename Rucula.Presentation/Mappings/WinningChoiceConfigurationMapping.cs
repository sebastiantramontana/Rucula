using Rucula.Presentation.Format;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal class WinningChoiceConfigurationMapping(ISpanishPriceFormatter formatter, IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<WinningChoiceViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<WinningChoiceViewModel> modelMapper)
        => modelMapper
            .MapValue(w => w.Name).ToElements.ById("winner-name").ToContent
            .MapValue(w => formatter.Format(w.DolarPrice)).ToElements.ById("winner-price").ToContent
            .Data;
}
