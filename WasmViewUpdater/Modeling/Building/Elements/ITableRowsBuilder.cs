using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling.Building.Elements
{
    public interface ITableRowsBuilder<TViewModel>
    {
        IModelBuilder<TViewModel, ElementQuerySelector> RowsFrom(RowSelector rowSelection);
    }
}
