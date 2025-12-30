namespace Rucula.Presentation.ViewModels;

public sealed class RuculaScreenViewModel
{
    public bool IsRunning { get; private set; } = false;

    public void Update(bool isRunning)
        => IsRunning = isRunning;
}
