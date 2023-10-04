using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface ITitulosService
    {
        Task<IEnumerable<Titulo>> GetAllTitulos();
        Task<IEnumerable<TituloIsin>> GetCclRankingTitulosIsin(Blue blue);
    }
}
