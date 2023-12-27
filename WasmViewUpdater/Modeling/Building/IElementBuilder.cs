using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building;

public interface IElementBuilder<TViewModel>
{
    IElementAttributeBuilder<TViewModel> ToElement(ElementSelector selector);
    IELementContentBuilder<TViewModel> ToContainerElement(ElementSelector selector);
}
