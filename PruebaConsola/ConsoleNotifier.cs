using Rucula.Domain.Abstractions;

namespace PruebaConsola;

public class ConsoleNotifier : INotifier
{
    public Task Notify(string message)
    {
        CleanConsoleLine();
        Console.Write(message);

        return Task.CompletedTask;
    }

    private static void CleanConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write("\u001b[2K");
    }
}