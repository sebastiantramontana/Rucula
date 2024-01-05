using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal interface IJsQueryElementsOneTimeOnDemandGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector);
}
