using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IBuildingValueModel<TFinalizable>
    {
        IToElementModel<TFinalizable> ToElement(IElementSelector selector);
        IToContainerElementModel<TFinalizable> ToContainerElement(IElementSelector selector);
    }
}
