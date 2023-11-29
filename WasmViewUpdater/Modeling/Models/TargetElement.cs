using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class TargetElement(ElementSelector Selector, ValueModel Parent)
    {
        internal ElementPlace Place { get; set; } = default!;
    }
}
