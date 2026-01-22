using Rucula.Application;

namespace Rucula.WebAssembly;

public sealed class RestartingPeriodicRunnerServiceMock : IRestartingPeriodicRunnerService
{
    public Task Restart(Func<Task> executeFunc, TimeSpan interval)
        => executeFunc.Invoke();
}
