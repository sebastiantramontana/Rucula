using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling.Building.Elements
{
    public interface ITableRowsBuilder<TViewModel>
    {
        IModelBuilder<TViewModel> RowsFrom(RowSelector rowSelection);
    }
}
