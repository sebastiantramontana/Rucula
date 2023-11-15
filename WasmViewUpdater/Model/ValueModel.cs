namespace WasmViewUpdater.Model
{
    internal record class ValueModel<TEntity, TReturn>(string propertyName, Func<TEntity, TReturn> valueFunc, string parentNode);
}
