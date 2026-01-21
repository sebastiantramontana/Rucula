using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal interface IParametersProvider
{
    Result<OptionParameters> GetParameters();
    bool AreDirty { get; }
}
