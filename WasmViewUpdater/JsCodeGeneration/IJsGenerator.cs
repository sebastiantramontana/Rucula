using Vitraux.Modeling.Building;

namespace Vitraux.JsCodeGeneration;

internal interface IJsGenerator<TViewModel>
{
    string GenerateJsCode(IModelBuilderData modelBuilderData);
}
