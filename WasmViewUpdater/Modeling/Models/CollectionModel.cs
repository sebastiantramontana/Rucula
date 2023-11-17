namespace WasmViewUpdater.Modeling.Models
{
    internal record class CollectionModel(Delegate CollectionFunc, string ParentNode) : IModel;
}
