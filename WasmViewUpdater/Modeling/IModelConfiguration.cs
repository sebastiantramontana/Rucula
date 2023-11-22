using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public interface IModelConfiguration<TViewModel>
    {
        internal IModelBuilder<TViewModel> Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorFactory elementSelectorFactory, IRowSelectionFactory rowSelectionFactory);
    }
}
