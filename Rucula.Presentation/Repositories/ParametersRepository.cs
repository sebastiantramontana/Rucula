using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal class ParametersRepository : IParametersRepository
{
    private Result<OptionParameters> _parameters = default;

    public bool AreDirty { get; private set; } = false;

    public Result<OptionParameters> GetParameters()
        => _parameters;

    public void StoreParameters(Result<OptionParameters> parameters, bool areDirty)
    {
        _parameters = parameters;
        AreDirty = areDirty;
    }

    public void Clean()
        => AreDirty = false;
}
