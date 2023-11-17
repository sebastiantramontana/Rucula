namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsExecutor<TEntity>
    {
        void Execute(TEntity entity);
    }
}
