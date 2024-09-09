using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface IChoicesService
    {
        Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions);
        ChoicesInfo RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions);
    }
}
