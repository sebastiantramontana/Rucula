namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IToContainerElementModel<TFinalizable> : IToElementModel<TFinalizable>
    {
        TFinalizable ToContent();
    }
}
