using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal interface IJsQueryElementsAlwaysGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector);
}
