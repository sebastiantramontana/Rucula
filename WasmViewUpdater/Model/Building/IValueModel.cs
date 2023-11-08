using WasmViewUpdater.Model.Selectors;

namespace WasmViewUpdater.Model.Building
{
    public interface IValueModel<TFinalizable>
        where TFinalizable : IModel
    {
        IToVoidElementModel<TFinalizable> ToVoidElement(ISelector selector);
        IToContainerElementModel<TFinalizable> ToContainerElement(ISelector selector);
    }
}
