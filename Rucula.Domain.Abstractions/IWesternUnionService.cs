using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IWesternUnionService
{
    Task<Optional<DolarWesternUnion>> GetDolarWesternUnion(WesternUnionParameters parameters);
}
