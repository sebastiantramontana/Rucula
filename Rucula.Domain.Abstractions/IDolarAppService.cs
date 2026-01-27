using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IDolarAppService
{
    Task<Optional<DolarApp>> GetDolarApp(DolarAppParameters parameters, Action<Optional<DolarApp>> notifyFunc);
}
