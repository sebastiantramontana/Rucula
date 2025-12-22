namespace Rucula.Presentation.ViewModels;

public sealed class RuculaScreenViewModel
{
    public bool IsRunning { get; private set; }

    public void Update(bool isRunning)
        => IsRunning = isRunning;
}
