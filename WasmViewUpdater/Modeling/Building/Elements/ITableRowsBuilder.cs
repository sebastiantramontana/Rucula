using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface ITableRowsBuilder<TViewModel>
    {
        IModelBuilder<TViewModel> RowsFrom(RowSelector rowSelection);
    }
}
