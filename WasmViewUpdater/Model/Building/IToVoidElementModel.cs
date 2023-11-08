namespace WasmViewUpdater.Model.Building
{
    public interface IToVoidElementModel<TFinalizable>
        where TFinalizable : IModel
    {
        TFinalizable ToAttribute(string attribute);
    }
}
