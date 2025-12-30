using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface ITitulosService
{
    Task<IEnumerable<TituloIsin>> GetNetCclRanking(Optional<Blue> blue, BondCommissions bondCommissions, Func<IEnumerable<TituloIsin>, Task> notifyFunc);
}
