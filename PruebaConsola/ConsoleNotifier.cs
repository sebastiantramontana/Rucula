using Rucula.Domain.Abstractions;

namespace PruebaConsola;

public class ConsoleNotifier : INotifier
{
    public void NotifyProgress(string message)
    {
        CleanConsoleLine();
        Console.Write(message);
    }

    private static void CleanConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write("\u001b[2K");
    }
}