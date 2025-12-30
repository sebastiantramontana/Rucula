namespace Rucula.Presentation.ViewModels;

internal sealed class NotifyProgressViewModel
{
    internal string Message { get; private set; } = string.Empty;

    internal void Update(string message)
        => Message = message;
}
