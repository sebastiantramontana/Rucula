using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class PeriodicChoicesService(IChoicesService choicesService, IPeriodicRunnerService periodicRunnerService) : IPeriodicChoicesService
{
    public async Task StartProcessChoices(ChoicesParameters parameters, TimeSpan interval, ChoicesCallbacks choicesCallbacks)
    {
        await periodicRunnerService.Stop();
        await periodicRunnerService.Start(() => choicesService.ProcessChoices(parameters, choicesCallbacks), interval);
    }
}
