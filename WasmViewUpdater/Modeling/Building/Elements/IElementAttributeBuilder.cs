namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IElementAttributeBuilder<TFinalizable>
    {
        TFinalizable ToAttribute(string attribute);
    }
}
