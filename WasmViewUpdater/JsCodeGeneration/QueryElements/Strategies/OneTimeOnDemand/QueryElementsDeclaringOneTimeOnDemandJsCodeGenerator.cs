using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandJsCodeGenerator(IJsQueryElementsOneTimeOnDemandGeneratorFactory factory) : IQueryElementsDeclaringOneTimeOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector elementSelector)
        => factory
            .GetInstance(elementSelector)
            .GenerateJsCode(elementObjectName, parentObjectName, elementSelector);
}
