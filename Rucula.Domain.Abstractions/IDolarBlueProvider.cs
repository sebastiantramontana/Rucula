using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public interface IDolarBlueProvider
{
    Task<Optional<Blue>> GetCurrentBlue(Func<Optional<Blue>, Task> notifyFunc);
}
