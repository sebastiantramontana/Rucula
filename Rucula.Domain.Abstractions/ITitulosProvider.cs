using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface ITitulosProvider : IProvider<Titulo>
{
    Task<IEnumerable<Titulo>> GetBonos();
    Task<IEnumerable<Titulo>> GetLetras();
}
