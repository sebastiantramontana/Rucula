using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IWesternUnionPresenter
{
    Task ShowWesternUnion(Optional<DolarWesternUnion> dolarWesternUnion);
}
