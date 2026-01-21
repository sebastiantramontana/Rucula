using Rucula.Domain.Entities.Parameters;

namespace Rucula.Application;

public interface IBestOptionService
{
    Task ProcessOptions(OptionParameters parameters, OptionCallbacks optionCallbacks);
}
