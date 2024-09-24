using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarDiarcoProvider
{
    Task<Optional<DolarDiarco>> GetCurrentDolarDiarco();
}
