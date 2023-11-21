namespace WasmViewUpdater.Modeling.Models;

internal class ValueModel(Delegate valueFunc)
{
    public Delegate ValueFunc { get; set; } = valueFunc;
    public IEnumerable<TargetElement> TargetElements { get; set; } = Enumerable.Empty<TargetElement>();
}
