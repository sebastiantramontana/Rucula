using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations;

internal sealed class PeriodicRunnerService : IPeriodicRunnerService
{
    private CancellationTokenSource? _cancellationTokenSouce;

    public async Task Start(Func<Task> executeFunc, TimeSpan interval)
    {
        using var timer = new PeriodicTimer(interval);
        _cancellationTokenSouce = new();

        try
        {
            do
            {
                await executeFunc.Invoke();
            } while (await timer.WaitForNextTickAsync(_cancellationTokenSouce.Token));
        }
        catch (OperationCanceledException)
        {
            _cancellationTokenSouce?.Dispose();
            _cancellationTokenSouce = null;
        }
    }

    public Task Stop()
        => (_cancellationTokenSouce?.IsCancellationRequested ?? true)
            ? Task.CompletedTask
            : _cancellationTokenSouce?.CancelAsync() ?? Task.CompletedTask;
}
