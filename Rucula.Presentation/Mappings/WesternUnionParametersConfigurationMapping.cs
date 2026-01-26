using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class WesternUnionParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<WesternUnionParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<WesternUnionParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapParameterValue(wu => wu.AmountToSend, InputParameterIds.WesternUnionVolumeInputParameterId)
            .MapParametersRange([InputParameterIds.WesternUnionVolumeInputParameterId])
            .Data;
}