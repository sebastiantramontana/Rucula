using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class BondParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<BondParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<BondParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapValue(b => b.PurchasePercentage).ToElements.ById("commission-porcentage-purchase-bond").ToAttribute("value")
            .MapValue(b => b.SalePercentage).ToElements.ById("commission-porcentage-sale-bond").ToAttribute("value")
            .MapValue(b => b.WithdrawalPercentage).ToElements.ById("commission-porcentage-withdrawal-bond").ToAttribute("value")
            .Data;
}
