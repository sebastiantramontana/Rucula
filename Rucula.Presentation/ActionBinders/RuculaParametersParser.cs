using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ActionBinders.Constants.ActionParameterKeys;

namespace Rucula.Presentation.ActionBinders;

internal sealed partial class RuculaParametersParser : IRuculaParametersParser
{
    public Result<OptionParameters> Parse(IDictionary<string, IEnumerable<string>> parameters, ParameterRange bondParameterRange, ParameterRange cryptoParameterRange, ParameterRange wuParameterRange, ParameterRange dolarAppParameterRange)
    {
        Result<OptionParameters> result;

        try
        {
            var bondCommissions = ParseBondCommissions(parameters, bondParameterRange);
            var cryptoParameters = ParseCryptoParameters(parameters, cryptoParameterRange);
            var westernUnionParameters = ParseWesternUnionParameters(parameters, wuParameterRange);
            var dolarAppParameters = ParseDolarAppParameters(parameters, dolarAppParameterRange);

            result = Result<OptionParameters>.Success(new(bondCommissions, cryptoParameters, westernUnionParameters, dolarAppParameters));
        }
        catch (ArgumentException ex)
        {
            result = Result<OptionParameters>.Failure(ex);
        }

        return result;
    }

    private static BondCommissions ParseBondCommissions(IDictionary<string, IEnumerable<string>> parameters, ParameterRange range)
    {
        var commissionPurchaseBond = ParseParameter(BondCommissionActionParameterKeys.PurchasePercentage, parameters, range);
        var commissionSaleBond = ParseParameter(BondCommissionActionParameterKeys.SalePercentage, parameters, range);
        var commissionWithdrawalBond = ParseParameter(BondCommissionActionParameterKeys.WithdrawalPercentage, parameters, range);

        return new(commissionPurchaseBond, commissionSaleBond, commissionWithdrawalBond);
    }

    private static WesternUnionParameters ParseWesternUnionParameters(IDictionary<string, IEnumerable<string>> parameters, ParameterRange range)
        => new(ParseParameter(WesternUnionActionParameterKeys.AmountToSend, parameters, range));

    private static DolarCryptoParameters ParseCryptoParameters(IDictionary<string, IEnumerable<string>> parameters, ParameterRange range)
        => new(ParseParameter(CryptoActionParameterKeys.TradingVolume, parameters, range));

    private static DolarAppParameters ParseDolarAppParameters(IDictionary<string, IEnumerable<string>> parameters, ParameterRange range)
        => new(ParseParameter(DolarAppActionParameterKeys.Volume, parameters, range));

    private static double ParseParameter(string paramKey, IDictionary<string, IEnumerable<string>> parameters, ParameterRange range)
    {
        if (!parameters.TryGetValue(paramKey, out var valueArray))
            throw new ArgumentException($"Parametro inválido: Se esperaba el parametro '{paramKey}'", paramKey);

        if (valueArray is null)
            throw new ArgumentNullException($"Parametro inválido: Se esperaba que la collección de valores NO fuera nula para el argumento '{paramKey}'", paramKey);

        if (valueArray.Count() != 1)
            throw new ArgumentException($"Parametro inválido: Se esperaba estrictamente un solo valor para el parametro '{paramKey}'. Valores del parametro: {string.Join(',', valueArray)}", paramKey);

        var valueString = valueArray.SingleOrDefault()
            ?? throw new ArgumentNullException($"Parametro inválido: Se esperaba que el valor NO fuera nulo para el argumento '{paramKey}'", paramKey);

        if (!double.TryParse(valueString, out var value))
            throw new ArgumentException($"Parametro inválido: Se esperaba un valor numérico para el parametro '{paramKey}'. Valor del parametro '{valueString}'", paramKey);

        return range.Contains(value)
            ? value
            : throw new ArgumentOutOfRangeException($"Parametro inválido: parametro '{paramKey}' fuera de rango: El valor es {value}, pero el rango permitido es: {range}");
    }
}
