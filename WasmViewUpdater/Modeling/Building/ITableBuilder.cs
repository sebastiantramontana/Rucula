using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building;

public interface ITableBuilder<TViewModel>
{
    ITableRowsBuilder<TViewModel> ToTable(ElementSelector selector);
}