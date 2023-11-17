namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IToContainerElementModel<TFinalizable> : IToElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToContent();
    }
}
