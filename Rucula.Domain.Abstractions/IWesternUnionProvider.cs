using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IWesternUnionProvider
{
    Task<Optional<DolarWesternUnionInfo>> GetCurrentDolarWesternUnion(WesternUnionParameters westernUnionParameters);
}
