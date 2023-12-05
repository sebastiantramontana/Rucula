using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building;

public interface IElementBuilder<TFinalizable>
{
    IElementAttributeBuilder<TFinalizable> ToElement(ElementSelector selector);
    IELementContentBuilder<TFinalizable> ToContainerElement(ElementSelector selector);
}
