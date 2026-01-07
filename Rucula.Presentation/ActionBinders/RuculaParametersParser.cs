using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ActionBinders.Constants;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaParametersParser : IRuculaParametersParser
{
    public Result<ChoicesParameters> Parse(IDictionary<string, IEnumerable<string>> parameters)
    {
        Result<ChoicesParameters> result;

        try
        {
            var bondCommissions = ParseBondCommissions(parameters);
            var westernUnionParameters = ParseWesternUnionParameters(parameters);
            var cryptoParameters = ParseDolarCryptoParameters(parameters);
            result = Result<ChoicesParameters>.Success(new(bondCommissions, cryptoParameters, westernUnionParameters));
        }
        catch (ArgumentException ex)
        {
            result = Result<ChoicesParameters>.Failure(ex);
        }

        return result;
    }

    private static BondCommissions ParseBondCommissions(IDictionary<string, IEnumerable<string>> parameters)
    {
        var range = new ParameterRange(0.0, 5.0);

        var commissionPurchaseBond = ParseParameter(BondCommissionActionParameterKeys.PurchasePercentage, parameters, range);
        var commissionSaleBond = ParseParameter(BondCommissionActionParameterKeys.SalePercentage, parameters, range);
        var commissionWithdrawalBond = ParseParameter(BondCommissionActionParameterKeys.WithdrawalPercentage, parameters, range);

        return new(commissionPurchaseBond, commissionSaleBond, commissionWithdrawalBond);
    }

    private static WesternUnionParameters ParseWesternUnionParameters(IDictionary<string, IEnumerable<string>> parameters)
        => new(ParseParameter(WesternUnionActionParameterKeys.AmountToSend, parameters, new ParameterRange(100.0, 10000.0)));

    private static DolarCryptoParameters ParseDolarCryptoParameters(IDictionary<string, IEnumerable<string>> parameters)
        => new(ParseParameter(CryptoActionParameterKeys.TradingVolume, parameters, new ParameterRange(100.0, 10000.0)));

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

    private readonly record struct ParameterRange(double Min, double Max)
    {
        internal bool Contains(double value)
            => (!double.IsNaN(value)) && value >= Min && value <= Max;

        public override string ToString()
            => $"{Min} - {Max}";
    }
}
