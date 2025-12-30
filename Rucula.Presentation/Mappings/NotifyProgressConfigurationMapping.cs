using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class NotifyProgressConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<NotifyProgressViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<NotifyProgressViewModel> modelMapper)
        => modelMapper
            .MapValue(n => n.Message).ToElements.ById("notify-progress").ToContent
            .Data;
}
