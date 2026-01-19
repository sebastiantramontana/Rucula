using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class CryptoParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<CryptoParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<CryptoParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapValue(c => c.Volume).ToElements.ById("trading-volume-crypto").ToAttribute("value")
            .Data;
}
