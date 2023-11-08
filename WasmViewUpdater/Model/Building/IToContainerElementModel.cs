namespace WasmViewUpdater.Model.Building
{
    public interface IToContainerElementModel<TFinalizable> : IToVoidElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToContent();
    }
}
