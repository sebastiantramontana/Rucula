using WasmViewUpdater.Model.Building.Elements;
using WasmViewUpdater.Model.Building.Selectors.Elements;

namespace WasmViewUpdater.Model.Building
{
    public interface IValueModel<TFinalizable>
        where TFinalizable : IModel
    {
        IToVoidElementModel<TFinalizable> ToVoidElement(IElementSelector selector);
        IToContainerElementModel<TFinalizable> ToContainerElement(IElementSelector selector);
    }
}
