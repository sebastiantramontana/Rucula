using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class JsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory(
    IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator jsQueryElementsByIdGenerator,
    IQueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator jsQueryElementsByQuerySelectorGenerator)
    : IJsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory
{
    public IQueryElementsDeclaringJsCodeGenerator GetInstance(FromTemplateAppendToElementSelection selectionBy)
     => selectionBy switch
     {
         FromTemplateAppendToElementSelection.Id => jsQueryElementsByIdGenerator,
         FromTemplateAppendToElementSelection.QuerySelector => jsQueryElementsByQuerySelectorGenerator,
         _ => throw new NotImplementedException($"IQueryElementsDeclaringJsCodeGenerator not implemented for FromTemplateElementSelection: {selectionBy}")
     };
}
