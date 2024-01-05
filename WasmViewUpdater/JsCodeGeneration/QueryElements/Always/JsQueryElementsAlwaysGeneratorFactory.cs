using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class JsQueryElementsAlwaysGeneratorFactory(
    IQueryElementsDeclaringAlwaysByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator,
    IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator jsQueryElementsByTemplateGenerator) : IJsQueryElementsAlwaysGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(ElementSelector elementSelector)
        => elementSelector.SelectionBy switch
        {
            ElementSelection.Id => jsQueryElementsByIdGenerator,
            ElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
            ElementSelection.Template => jsQueryElementsByTemplateGenerator,
            _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for ElementSelector: {elementSelector}")
        };
}
