namespace Rucula.Domain.Abstractions;

public interface IPeriodicRunnerService
{
    Task Start(Func<Task> executeFunc, TimeSpan interval);
    Task Stop();
}