using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.Presenters;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class RuculaScreenConfigurationMapping(
    IConfigurationBehaviorProvider behaviorProvider,
    IRuculaScreenPresenter ruculaScreenPresenter) : IViewModelConfiguration<RuculaScreenViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<RuculaScreenViewModel> modelMapper)
        => modelMapper
            .MapActionAsync(ruculaScreenPresenter.StartShowChoices).FromInputs.ById("refresh-button").On("click")
            .MapActionAsync<IRuculaValidateParametersActionBinderAsync>().FromInputs.ByQuery("[data-input-parameter]").On("input").AddRuculaParameters()
            .MapValue(vm => vm.IsRunning)
                .ToElements.ById("loading-indicator").ToAttribute("data-inline-block")
                .ToElements.ByQuery("[data-input-parameter]").ToAttribute("disabled")
            .MapValue(vm => !vm.IsRunning)
                .ToElements.ById("all-indicators-container").ToAttribute("data-hidden")
                .ToElements.ById("winning-choice").ToAttribute("data-inline-block")
            .MapValue(vm => vm.IsRunning || vm.AreParametersInvalid)
                .ToElements.ById("refresh-button").ToAttribute("disabled")
            .MapValue(CheckSaveParametersDisabled)
                .ToElements.ByQuery("[data-save-parameters]").ToAttribute("disabled")
            .Data;

    private static bool CheckSaveParametersDisabled(RuculaScreenViewModel vm)
        => vm.IsRunning || (!vm.IsRunning && (vm.AreParametersInvalid || !vm.AreParametersDirty));
}
