using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions
{
    public interface IWesternUnionProvider
    {
        Task<DolarWesternUnion> GetCurrentDolarWesternUnion();
    }
}
