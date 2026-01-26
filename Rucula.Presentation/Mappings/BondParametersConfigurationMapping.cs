using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Mappings;

internal sealed class BondParametersConfigurationMapping(IConfigurationBehaviorProvider behaviorProvider) : IViewModelConfiguration<BondParameterValuesViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = behaviorProvider.DefaultConfigurationBehavior;

    public ModelMappingData ConfigureMapping(IModelMapper<BondParameterValuesViewModel> modelMapper)
        => modelMapper
            .MapParameterValue(b => b.PurchasePercentage, InputParameterIds.BondPurchaseInputParameterId)
            .MapParameterValue(b => b.SalePercentage, InputParameterIds.BondSaleInputParameterId)
            .MapParameterValue(b => b.WithdrawalPercentage, InputParameterIds.BondWithdrawalInputParameterId)
            .MapParametersRange([InputParameterIds.BondPurchaseInputParameterId, InputParameterIds.BondSaleInputParameterId, InputParameterIds.BondWithdrawalInputParameterId])
            .Data;
}
