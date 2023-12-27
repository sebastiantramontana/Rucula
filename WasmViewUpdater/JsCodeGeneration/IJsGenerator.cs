using Vitraux.Modeling.Building;

namespace Vitraux.JsCodeGeneration
{
    internal interface IJsGenerator<TViewModel>
    {
        IJsExecutor<TViewModel> GenerateJsCode(IModelBuilderData modelBuilderData);
    }
}
