using Rucula.Presentation.ActionBinders.Constants.ActionParameterKeys;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Rucula.Presentation.Mappings;

public static class RuculaParametersActionMappingExt
{
    public static IRootActionAddParameterNameFinallizableBuilder<TViewModel> AddRuculaParameters<TViewModel>(this IRootActionSourceOrParametersBuilder<TViewModel>
        builder) => builder
            .AddParameter(BondCommissionActionParameterKeys.PurchasePercentage).FromParamInputs.ById("commission-porcentage-purchase-bond")
            .AddParameter(BondCommissionActionParameterKeys.SalePercentage).FromParamInputs.ById("commission-porcentage-sale-bond")
            .AddParameter(BondCommissionActionParameterKeys.WithdrawalPercentage).FromParamInputs.ById("commission-porcentage-withdrawal-bond")
            .AddParameter(CryptoActionParameterKeys.TradingVolume).FromParamInputs.ById("trading-volume-crypto")
            .AddParameter(WesternUnionActionParameterKeys.AmountToSend).FromParamInputs.ById("amount-to-send-wu");
}