namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IELementContentBuilder<TFinalizable> : IElementAttributeBuilder<TFinalizable>
    {
        TFinalizable ToContent();
    }
}
