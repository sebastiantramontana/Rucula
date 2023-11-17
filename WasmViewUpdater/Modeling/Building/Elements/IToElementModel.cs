namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IToElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToAttribute(string attribute);
    }
}
