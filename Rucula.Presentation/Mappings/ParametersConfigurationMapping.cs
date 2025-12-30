using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class ParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<ParametersViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<ParametersViewModel> modelMapper)
        => modelMapper
            .MapActionAsync<IRuculaParametersActionBinderAsync>()
                .FromInputs.ByQuery("button[data-save-parameters]").On("click")
                .FromInputs.ByQuery("input[data-input-parameter]").On("input").AddRuculaParameters()
            .MapValue(p => p.Bonds).ToSaveParametersJs()
            .MapValue(p => p.Cryptos).ToSaveParametersJs()
            .MapValue(p => p.WesternUnion).ToSaveParametersJs()
            .MapValue(p => p.AreValid).ToElements.ByQuery("button[data-save-parameters]").ToAttribute("disabled")
            .Data;
}
