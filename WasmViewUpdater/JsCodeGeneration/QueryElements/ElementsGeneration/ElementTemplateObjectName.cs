using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class ElementTemplateObjectName : ElementObjectName
{
    internal ElementTemplateObjectName(string Name, ElementTemplateSelector AssociatedSelector)
        : base(Name, AssociatedSelector)
    {
    }

    internal required string AppendToName { get; init; }
    internal required string ToChildName { get; init; }
}