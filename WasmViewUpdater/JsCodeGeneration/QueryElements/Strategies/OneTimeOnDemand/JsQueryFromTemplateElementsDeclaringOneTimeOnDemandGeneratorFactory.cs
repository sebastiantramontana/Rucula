using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class JsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory(
    IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(FromTemplateElementSelection selectionBy)
     => selectionBy switch
     {
         FromTemplateElementSelection.Id => jsQueryElementsByIdGenerator,
         FromTemplateElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
         _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
     };
}
