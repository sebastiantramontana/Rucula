using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IPeriodicChoicesService
{
    Task StartProcessChoices(ChoicesParameters parameters, TimeSpan interval, ChoicesCallbacks choicesCallbacks);
}