using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface ITitulosService
    {
        Task<IEnumerable<TituloIsin>> GetNetCclRanking(Blue blue, BondCommissions bondCommissions);
        IEnumerable<TituloIsin> RecalculateNetCclRanking(IEnumerable<TituloIsin> titulos, BondCommissions bondCommissions);
    }
}
