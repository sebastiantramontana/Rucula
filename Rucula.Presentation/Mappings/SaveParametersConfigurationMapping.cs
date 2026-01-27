using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class SaveParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider, IRuculaParametersSaver ruculaParametersSaver) : IViewModelConfiguration<SaveParametersViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<SaveParametersViewModel> modelMapper)
        => modelMapper
            .MapActionAsync(p => ruculaParametersSaver.Save()).FromInputs.ByQuery("[data-save-parameters]").On("click")
            .MapValue(p => p.Bonds).ToSaveParametersJs()
            .MapValue(p => p.Cryptos).ToSaveParametersJs()
            .MapValue(p => p.WesternUnion).ToSaveParametersJs()
            .MapValue(p => p.DolarApp).ToSaveParametersJs()
            .Data;
}
