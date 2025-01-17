namespace Rucula.Domain.Abstractions;

public interface INotifier
{
    Task Notify(string message);
}