using Rucula.Presentation.ActionBinders.Constants.ActionParameterKeys;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Rucula.Presentation.Mappings;

public static class RuculaParametersActionMappingExt
{
    public static IRootActionAddParameterNameFinallizableBuilder<TViewModel> AddRuculaParameters<TViewModel>(this IRootActionSourceOrParametersBuilder<TViewModel>
        builder) => builder
            .AddParameter(BondCommissionActionParameterKeys.PurchasePercentage).FromParamInputs.ById(InputParameterIds.BondPurchaseInputParameterId)
            .AddParameter(BondCommissionActionParameterKeys.SalePercentage).FromParamInputs.ById(InputParameterIds.BondSaleInputParameterId)
            .AddParameter(BondCommissionActionParameterKeys.WithdrawalPercentage).FromParamInputs.ById(InputParameterIds.BondWithdrawalInputParameterId)
            .AddParameter(CryptoActionParameterKeys.TradingVolume).FromParamInputs.ById(InputParameterIds.CryptoVolumeInputParameterId)
            .AddParameter(WesternUnionActionParameterKeys.AmountToSend).FromParamInputs.ById(InputParameterIds.WesternUnionVolumeInputParameterId);
}