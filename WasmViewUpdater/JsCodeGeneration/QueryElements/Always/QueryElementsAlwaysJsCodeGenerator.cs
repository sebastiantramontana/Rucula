using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class QueryElementsAlwaysJsCodeGenerator(IJsQueryElementsAlwaysGeneratorFactory jsQueryElementsGeneratorFactory)
    : QueryElementsJsCodeGeneratorBase, IQueryElementsAlwaysJsCodeGenerator
{
    protected override string GenerateJsCodeLine(string elementName, string parentObjectName, ElementSelector selector)
        => jsQueryElementsGeneratorFactory
            .GetInstance(selector)
            .GenerateJsCode(elementName, parentObjectName, selector);
}


