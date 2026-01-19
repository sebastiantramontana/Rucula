using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal interface IParametersRepository : IParametersProvider
{
    void StoreParameters(Result<ChoicesParameters> parameters, bool areDirty);
    void Clean();
}
