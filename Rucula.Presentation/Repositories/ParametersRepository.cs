using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal class ParametersRepository : IParametersRepository
{
    private Result<ChoicesParameters> _parameters = default;

    public bool AreDirty { get; private set; } = false;

    public Result<ChoicesParameters> GetParameters()
        => _parameters;

    public void StoreParameters(Result<ChoicesParameters> parameters, bool areDirty)
    {
        _parameters = parameters;
        AreDirty = areDirty;
    }

    public void Clean()
        => AreDirty = false;
}
