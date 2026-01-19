using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class WesternUnionParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<WesternUnionParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<WesternUnionParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapValue(wu => wu.AmountToSend).ToElements.ById("amount-to-send-wu").ToAttribute("value")
            .Data;
}