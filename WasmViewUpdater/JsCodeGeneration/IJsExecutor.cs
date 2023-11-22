namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsExecutor<TViewModel>
    {
        void Execute(TViewModel viewModel);
    }
}
