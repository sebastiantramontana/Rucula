using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class TargetElement(IElementSelector selector, ValueModel parent)
    {
        public IElementSelector Selector { get; } = selector;
        public ValueModel Parent { get; } = parent;
        public ElementPlace Place { get; set; } = default!;
    }
}
