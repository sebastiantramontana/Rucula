using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class ParametersViewModel
{
    internal BondParametersViewModel Bonds { get; private set; } = new();
    internal CryptoParametersViewModel Cryptos { get; private set; } = new();
    internal WesternUnionParametersViewModel WesternUnion { get; private set; } = new();
    internal bool AreValid { get; private set; } = false;

    internal void Update(bool areValid)
        => AreValid = areValid;

    internal void Update(ChoicesParameters parameters)
    {
        Bonds.Values.Update(parameters.BondCommissions);
        Cryptos.Values.Update(parameters.CryptoParameters);
        WesternUnion.Values.Update(parameters.WesternUnionParameters);
    }
}
