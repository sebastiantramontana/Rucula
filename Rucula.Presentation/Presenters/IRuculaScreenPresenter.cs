using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ViewModels;

namespace Rucula.Presentation.Presenters;

public interface IRuculaScreenPresenter
{
    Task StartShowChoices(RuculaScreenViewModel viewmodel, ChoicesParameters parameters);
    Task StartShowChoicesFromScratch(ChoicesParameters parameters);
}
