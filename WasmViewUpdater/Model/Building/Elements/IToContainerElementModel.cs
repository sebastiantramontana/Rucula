namespace WasmViewUpdater.Model.Building.Elements
{
    public interface IToContainerElementModel<TFinalizable> : IToVoidElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToContent();
    }
}
