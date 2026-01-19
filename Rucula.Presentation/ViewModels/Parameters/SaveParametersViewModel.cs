namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed record class SaveParametersViewModel(BondParametersViewModel Bonds = default!, CryptoParametersViewModel Cryptos = default!, WesternUnionParametersViewModel WesternUnion = default!);