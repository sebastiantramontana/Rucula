namespace Rucula.Domain.Abstractions;

public interface INotifier
{
    void NotifyProgress(string message);
}
