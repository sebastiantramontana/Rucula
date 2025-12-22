using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IBluePresenter
{
    Task ShowBlue(Optional<Blue> blue);
}
