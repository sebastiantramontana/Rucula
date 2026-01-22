namespace Rucula.Application;

public interface IRestartingPeriodicRunnerService
{
    Task Restart(Func<Task> executeFunc, TimeSpan interval);
}