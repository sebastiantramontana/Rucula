using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class TargetElement(ElementSelector selector, ValueModel parent)
    {
        public ElementSelector Selector { get; } = selector;
        public ValueModel Parent { get; } = parent;
        public ElementPlace Place { get; set; } = default!;
    }
}
