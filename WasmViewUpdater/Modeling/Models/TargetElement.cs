using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class TargetElement(IElementSelector Selector, ElementPlace Place);
}
