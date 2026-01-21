using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal interface IParametersRepository : IParametersProvider
{
    void StoreParameters(Result<OptionParameters> parameters, bool areDirty);
    void Clean();
}
