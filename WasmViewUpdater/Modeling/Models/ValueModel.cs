namespace WasmViewUpdater.Modeling.Models
{
    internal record class ValueModel(Delegate ValueFunc, string ParentNode, IEnumerable<TargetElement> TargetElements);
}
