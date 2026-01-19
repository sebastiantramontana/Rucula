using Rucula.Presentation.ViewModels;

namespace Rucula.Presentation.Presenters;

internal interface IRuculaScreenPresenter
{
    Task StartShowChoices(RuculaScreenViewModel viewmodel);
}
