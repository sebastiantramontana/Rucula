using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IChoicesService
{
    Task<ChoicesInfo> GetChoices(BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters);
    Task<ChoicesInfo> RecalculateChoices(ChoicesInfo choices, BondCommissions bondCommissions, WesternUnionParameters westernUnionParameters);
}
