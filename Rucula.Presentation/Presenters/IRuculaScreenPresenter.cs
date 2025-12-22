using Rucula.Domain.Entities;
using Rucula.Presentation.ViewModels;

namespace Rucula.Presentation.Presenters;

internal interface IRuculaScreenPresenter
{
    Task Start();
    Task ShowChoices(RuculaScreenViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters);
    Task ShowRecalculatedChoices(RuculaScreenViewModel viewmodel, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters, DolarCryptoParameters dolarCryptoParameters);
}
