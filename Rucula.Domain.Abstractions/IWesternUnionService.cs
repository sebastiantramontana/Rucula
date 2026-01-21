using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IWesternUnionService
{
    Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters, Action<Optional<DolarWesternUnion>> notifyFunc);
}
