using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal class ElementNamesGenerator : IElementNamesGenerator
{
    public IEnumerable<ElementObjectName> Generate(IEnumerable<ElementSelector> selectors)
        => selectors.Select((selector, indexAsPostfix) => new ElementObjectName($"element{indexAsPostfix}", selector));
}

