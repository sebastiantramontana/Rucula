namespace WasmViewUpdater.Model.Building.Elements
{
    public interface IToVoidElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToAttribute(string attribute);
    }
}
