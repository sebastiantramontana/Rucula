namespace Rucula.Domain.Abstractions;

public interface INotifier
{
    Task NotifyProgress(string message);
}
