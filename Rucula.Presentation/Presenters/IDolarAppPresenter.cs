using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IDolarAppPresenter
{
    Task ShowDolarApp(Optional<DolarApp> dolarApp);
}
