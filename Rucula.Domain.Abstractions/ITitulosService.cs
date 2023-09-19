using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface ITitulosService
    {
        IEnumerable<Titulo> GetAllTitulos();
        IEnumerable<TituloIsin> GetAllTitulosIsin();
        IEnumerable<TituloIsin> GetPrunedTitulosIsin();
    }
}
