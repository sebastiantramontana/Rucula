namespace WasmViewUpdater.Modeling.Models
{
    internal record class ValueModel<TEntity, TReturn>(Func<TEntity, TReturn> ValueFunc, string ParentNode, IEnumerable<TargetElement> TargetElements);
}
