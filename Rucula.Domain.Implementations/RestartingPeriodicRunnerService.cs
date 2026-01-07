using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations;

internal sealed class RestartingPeriodicRunnerService(IPeriodicRunnerService periodicRunnerService) : IRestartingPeriodicRunnerService
{
    public async Task Restart(Func<Task> executeFunc, TimeSpan interval)
    {
        await periodicRunnerService.Stop();
        await periodicRunnerService.Start(executeFunc, interval);
    }
}
