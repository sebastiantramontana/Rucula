using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Repositories;

internal interface IParametersProvider
{
    Result<ChoicesParameters> GetParameters();
    bool AreDirty { get; }
}
