using Rucula.Domain.Entities;
using Rucula.Presentation.Constants;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaScreenParametersParser : IRuculaScreenParametersParser
{
    public RuculaScreenParameters Parse(IDictionary<string, IEnumerable<string>> parameters)
    {
        var bondCommissions = ParseBondCommissions(parameters);
        var westernUnionParameters = ParseWesternUnionParameters(parameters);
        var cryptoParameters = ParseDolarCryptoParameters(parameters);

        return new(bondCommissions, westernUnionParameters, cryptoParameters);
    }

    private static BondCommissions ParseBondCommissions(IDictionary<string, IEnumerable<string>> parameters)
    {
        var commissionPurchaseBond = ParseParameter(BondCommissionParameterKeys.PurchasePercentage, parameters);
        var commissionSaleBond = ParseParameter(BondCommissionParameterKeys.SalePercentage, parameters);
        var commissionWithdrawalBond = ParseParameter(BondCommissionParameterKeys.WithdrawalPercentage, parameters);

        return new(commissionPurchaseBond, commissionSaleBond, commissionWithdrawalBond);
    }

    private static WesternUnionParameters ParseWesternUnionParameters(IDictionary<string, IEnumerable<string>> parameters)
        => new(ParseParameter(WesternUnionParameterKeys.AmountToSend, parameters));

    private static DolarCryptoParameters ParseDolarCryptoParameters(IDictionary<string, IEnumerable<string>> parameters)
        => new(ParseParameter(CryptoParameterKeys.TradingVolume, parameters));

    private static double ParseParameter(string paramKey, IDictionary<string, IEnumerable<string>> parameters)
    {
        if (parameters.TryGetValue(paramKey, out var valueArray))
            throw new ArgumentException($"Error interno: Se esperaba el parametro '{paramKey}' para refrescar los datos", paramKey);

        if (valueArray is null)
            throw new ArgumentNullException($"Error interno: Se esperaba que la collección de valores NO fuera nula para el argumento '{paramKey}' para refrescar los datos", paramKey);

        if (valueArray.Count() != 1)
            throw new ArgumentException($"Error interno: Se esperaba estrictamente un solo valor para el parametro '{paramKey}' para refrescar los datos. Valores del parametro: {string.Join(',', valueArray)}", paramKey);

        var valueString = valueArray.Single()
            ?? throw new ArgumentNullException($"Error interno: Se esperaba que el valor NO fuera nulo para el argumento '{paramKey}' para refrescar los datos", paramKey);

        return double.TryParse(valueString, out var value)
            ? value
            : throw new ArgumentException($"Error interno: Se esperaba un valor numérico para el parametro '{paramKey}' para refrescar los datos. Valor del parametro '{valueString}'", paramKey);
    }
}
