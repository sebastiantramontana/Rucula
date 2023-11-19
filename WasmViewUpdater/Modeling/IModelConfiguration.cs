using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public interface IModelConfiguration<TEntity>
    {
        internal IModelBuilder<TEntity> Configure(IModelBuilder<TEntity> modelBuilder, IElementSelectorFactory elementSelectorFactory, IRowSelectionFactory rowSelectionFactory);
    }
}
