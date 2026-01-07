namespace Rucula.Presentation.ViewModels;

internal sealed record class NotifyProgressViewModel
{
    internal string Message { get; private set; } = string.Empty;

    internal void Update(string message)
        => Message = message;
}
