using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IWesternUnionProvider
{
    Task<Optional<DolarWesternUnionInfo>> GetCurrentDolarWesternUnion(WesternUnionParameters westernUnionParameters);
}
