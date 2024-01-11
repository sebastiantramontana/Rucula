using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal interface IJsQueryElementsDeclaringAlwaysGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector);
}
