using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarAppProvider
{
    Task<Optional<DolarAppInfo>> GetCurrentDolarApp();
}
