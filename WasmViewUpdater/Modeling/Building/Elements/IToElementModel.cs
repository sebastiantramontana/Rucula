namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IToElementModel<TFinalizable>
    {
        TFinalizable ToAttribute(string attribute);
    }
}
