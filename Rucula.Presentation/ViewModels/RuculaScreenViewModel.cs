namespace Rucula.Presentation.ViewModels;

internal sealed record class RuculaScreenViewModel(bool IsRunning = false, bool AreParametersInvalid = false, bool AreParametersDirty = false);