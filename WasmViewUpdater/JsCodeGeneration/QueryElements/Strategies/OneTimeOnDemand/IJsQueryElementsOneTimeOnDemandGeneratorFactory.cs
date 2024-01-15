using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal interface IJsQueryElementsOneTimeOnDemandGeneratorFactory
{
    IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector);
}
