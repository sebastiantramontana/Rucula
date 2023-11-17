using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IBuildingCollectionModel<TEntity>
    {
        IToTableModel<TEntity> ToTable(IElementSelector selector);
    }
}
