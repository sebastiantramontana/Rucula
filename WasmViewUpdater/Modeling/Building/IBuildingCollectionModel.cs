using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IBuildingCollectionModel<TViewModel>
    {
        IToTableModel<TViewModel> ToTable(ElementSelector selector);
    }
}
