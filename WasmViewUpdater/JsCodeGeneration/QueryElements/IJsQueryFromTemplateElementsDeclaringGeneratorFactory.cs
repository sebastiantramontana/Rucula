using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IJsQueryFromTemplateElementsDeclaringGeneratorFactory
    {
        IQueryElementsDeclaringJsCodeGenerator GetInstance(FromTemplateElementSelection selectionBy);
    }
}