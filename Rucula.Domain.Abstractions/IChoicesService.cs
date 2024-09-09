using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface IChoicesService
    {
        Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions);
    }
}
