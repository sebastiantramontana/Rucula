using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IBuildingValueModel<TFinalizable>
    {
        IToElementModel<TFinalizable> ToElement(ElementSelector selector);
        IToContainerElementModel<TFinalizable> ToContainerElement(ElementSelector selector);
    }
}
