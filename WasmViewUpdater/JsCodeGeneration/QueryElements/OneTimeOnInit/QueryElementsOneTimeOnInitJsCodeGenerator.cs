using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnInit;

internal class QueryElementsOneTimeOnInitJsCodeGenerator(IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator generator)
    : QueryElementsJsCodeGeneratorBase, IQueryElementsOneTimeOnInitJsCodeGenerator
{
    protected override string GenerateJsCodeLine(string elementName, string parentObjectName, ElementSelector selector)
        => generator.GenerateJsCode(elementName, parentObjectName, selector);
}


