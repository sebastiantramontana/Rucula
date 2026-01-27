using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class DolarAppParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<DolarAppParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<DolarAppParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapParameterValue(da => da.Volume, InputParameterIds.DolarAppVolumeInputParameterId)
            .MapParametersRange([InputParameterIds.DolarAppVolumeInputParameterId])
            .Data;
}
