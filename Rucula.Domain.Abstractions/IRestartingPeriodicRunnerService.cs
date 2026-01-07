namespace Rucula.Domain.Abstractions;

public interface IRestartingPeriodicRunnerService
{
    Task Restart(Func<Task> executeFunc, TimeSpan interval);
}