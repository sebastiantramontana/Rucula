namespace Vitraux.JsCodeGeneration
{
    internal interface IJsExecutor<TViewModel>
    {
        void Execute(TViewModel viewModel);
    }
}
