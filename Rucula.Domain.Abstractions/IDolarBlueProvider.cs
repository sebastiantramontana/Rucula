using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface IDolarBlueProvider
    {
        Task<Blue> GetCurrentBlue();
    }
}
