using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryAppendToElementsDeclaringByTemplateJsCodeGenerator : IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
{
    public string GenerateAppendToJsCode(string appendToObjectName, FromTemplateAppendToElementSelector elementToAppend, IJsQueryFromTemplateElementsDeclaringGeneratorFactory jsGeneratorFactory)
        => jsGeneratorFactory
            .GetInstance(elementToAppend.SelectionBy)
            .GenerateJsCode("document", MapElementObjectNameFromTemplateSelector(appendToObjectName, elementToAppend));

    private static ElementObjectName MapElementObjectNameFromTemplateSelector(string objectName, FromTemplateAppendToElementSelector fromTemplateElementSelector)
    {
        var newSelector = CreateElementSelectorFromTemplateSelector(fromTemplateElementSelector);
        return new ElementObjectName(objectName, newSelector);
    }

    private static ElementSelector CreateElementSelectorFromTemplateSelector(FromTemplateAppendToElementSelector selector)
        => selector.SelectionBy switch
        {
            FromTemplateAppendToElementSelection.Id => new ElementIdSelector(selector.Value),
            FromTemplateAppendToElementSelection.QuerySelector => new ElementQuerySelector(selector.Value),
            _ => throw new NotImplementedException($"{selector.SelectionBy} not implemented"),
        };
}
