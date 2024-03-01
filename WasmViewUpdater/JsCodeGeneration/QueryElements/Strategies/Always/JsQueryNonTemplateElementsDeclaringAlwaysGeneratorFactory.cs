using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class JsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(FromTemplateElementSelection selectionBy)
        => selectionBy switch
        {
            FromTemplateElementSelection.Id => jsQueryElementsByIdGenerator,
            FromTemplateElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
        };
}
