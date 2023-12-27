using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public interface IModelConfiguration<TViewModel>
    {
        internal void Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorBuilder elementSelectorBuilder, IRowSelectorBuilder rowSelectionBuilder);
    }
}
