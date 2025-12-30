using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class RuculaScreenConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<RuculaScreenViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<RuculaScreenViewModel> modelMapper)
        => modelMapper
            .MapActionAsync<IRuculaScreenRefreshActionBinderAsync>().FromInputs.ById("refresh-button").On("click").AddRuculaParameters()
            .MapValue(ci => ci.IsRunning)
                .ToElements.ByQuery("[data-disabled-when-running]").ToAttribute("disabled")
                .ToElements.ById("loading-indicator").ToAttribute("data-inline-block")
            .MapValue(ci => !ci.IsRunning)
                .ToElements.ById("all-indicators-container").ToAttribute("data-hidden")
                .ToElements.ById("winning-choice").ToAttribute("data-inline-block")
            .Data;
}
