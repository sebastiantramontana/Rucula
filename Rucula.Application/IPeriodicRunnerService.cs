namespace Rucula.Application;

public interface IPeriodicRunnerService
{
    Task Start(Func<Task> executeFunc, TimeSpan interval);
    Task Stop();
}