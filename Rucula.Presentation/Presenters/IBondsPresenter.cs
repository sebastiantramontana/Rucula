using Rucula.Domain.Entities;

namespace Rucula.Presentation.Presenters;

internal interface IBondsPresenter
{
    Task ShowBonds(IEnumerable<TituloIsin> bonds);
}
