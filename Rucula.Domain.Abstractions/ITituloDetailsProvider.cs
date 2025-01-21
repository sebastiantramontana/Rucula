using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface ITituloDetailsProvider
{
    Task<IEnumerable<TituloDetails>> GetTituloDetails(string symbol);
}
