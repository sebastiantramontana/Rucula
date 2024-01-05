using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal class QueryElementsOneTimeOnDemandJsCodeGenerator(IJsQueryElementsOneTimeOnDemandGeneratorFactory jsQueryElementsGeneratorFactory)
    : QueryElementsJsCodeGeneratorBase, IQueryElementsOneTimeOnDemandJsCodeGenerator
{
    protected override string GenerateJsCodeLine(string elementName, string parentObjectName, ElementSelector selector)
        => jsQueryElementsGeneratorFactory
            .GetInstance(selector)
            .GenerateJsCode(elementName, parentObjectName, selector);
}


