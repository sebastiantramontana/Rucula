using Rucula.Presentation.Constants;
using Rucula.Presentation.ViewModels;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Rucula.Presentation.Mappings;

public static class RuculaParametersMapping
{
    public static IRootActionAddParameterNameFinallizableBuilder<RuculaScreenViewModel> AddRuculaParameters(this IRootActionSourceOrParametersBuilder<RuculaScreenViewModel>
        builder) => builder
            .AddParameter(BondCommissionParameterKeys.PurchasePercentage).FromParamInputs.ById("commission-porcentage-purchase-bond")
            .AddParameter(BondCommissionParameterKeys.SalePercentage).FromParamInputs.ById("commission-porcentage-sale-bond")
            .AddParameter(BondCommissionParameterKeys.WithdrawalPercentage).FromParamInputs.ById("commission-porcentage-withdrawal-bond")
            .AddParameter(CryptoParameterKeys.TradingVolume).FromParamInputs.ById("trading-volume-crypto")
            .AddParameter(WesternUnionParameterKeys.AmountToSend).FromParamInputs.ById("amount-to-send-wu");
}