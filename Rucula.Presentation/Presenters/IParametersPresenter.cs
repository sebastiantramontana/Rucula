using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Presenters;

internal interface IParametersPresenter
{
    Task SaveParameters(Result<OptionParameters> parameters);
    Task UpdateUIStateByParameters(Result<OptionParameters> parameters, bool areParametersDirty);
    Task ShowBondParameters(BondCommissions bondCommissions);
    Task ShowCryptoParameters(DolarCryptoParameters cryptoParameters);
    Task ShowWesternUnionParameters(WesternUnionParameters wuParameters);
    Task ShowDolarAppParameters(DolarAppParameters dolarAppParameters);
}
