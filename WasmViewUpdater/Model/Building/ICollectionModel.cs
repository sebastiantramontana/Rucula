using WasmViewUpdater.Model.Building.Elements;
using WasmViewUpdater.Model.Building.Selectors.Elements;

namespace WasmViewUpdater.Model.Building
{
    public interface ICollectionModel<TEntity>
    {
        IToTableModel<TEntity> ToTable(IElementSelector selector);
    }
}
