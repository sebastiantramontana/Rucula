using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementJsCodeGenerator(
    IElementPlaceAttributeJsCodeGenerator attributeGenerator,
    IElementPlaceContentJsCodeGenerator contentGenerator)
    : ITargetElementJsCodeGenerator
{
    private const string Indentation = "    ";

    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string valueObjectName)
        => associatedElements
            .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(GeneratePlaceJsCode(targetElement.Place, ae.Name, valueObjectName)))
            .ToString();

    private string GeneratePlaceJsCode(ElementPlace elementPlace, string elementObjectName, string valueObjectName)
        => elementPlace.ElementPlacing switch
        {
            ElementPlacing.Attribute => Indentation + attributeGenerator.Generate(elementPlace.Value, elementObjectName, valueObjectName),
            ElementPlacing.Content => Indentation + contentGenerator.Generate(elementObjectName, valueObjectName),
            _ => throw new NotImplementedException($"ElementPlacing {elementPlace.ElementPlacing} not implemented in TargetElementJsCodeGeneration"),
        };
}
