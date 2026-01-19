using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Presenters;

internal interface IParametersPresenter
{
    Task SaveParameters(Result<ChoicesParameters> parameters);
    Task UpdateUIStateByParameters(Result<ChoicesParameters> parameters, bool areParametersDirty);
    Task ShowBondParameters(BondCommissions bondCommissions);
    Task ShowCryptoParameters(DolarCryptoParameters cryptoParameters);
    Task ShowWesternUnionParameters(WesternUnionParameters wuParameters);
}
