using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building;

public interface ITableBuilder<TViewModel>
{
    ITableRowsBuilder<TViewModel> ToTable(ElementSelector selector);
}