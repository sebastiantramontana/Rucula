using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(FromTemplateAppendToElementSelection selectionBy)
        => selectionBy switch
        {
            FromTemplateAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
            FromTemplateAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
        };
}
