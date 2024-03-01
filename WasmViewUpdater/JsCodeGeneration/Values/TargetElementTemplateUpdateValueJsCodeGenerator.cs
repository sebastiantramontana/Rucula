using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementTemplateUpdateValueJsCodeGenerator(
    IUpdateByTemplateCall updateByTemplateCall,
    IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
    IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
    ISetElementsAttributeCall setElementsAttributeCall,
    ISetElementsContentCall setElementsContentCall,
    ICodeFormatting codeFormatting)
    : ITargetElementTemplateUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string valueObjectName)
        => associatedElements
            .Cast<ElementTemplateObjectName>()
            .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(CreateUpdateByTemplateFunctionCall(targetElement, ae, valueObjectName)))
            .ToString();

    private string CreateUpdateByTemplateFunctionCall(TargetElement targetElement, ElementTemplateObjectName elementTemplateObjectName, string valueObjectName)
    {
        var templateSelector = elementTemplateObjectName.AssociatedSelector as ElementTemplateSelector;

        var toChildQueryFunctionCall = GenerateToChildQueryFunctionCall(templateSelector!.TargetChildElement);
        var updateTemplateChildFunctionCall = GenerateUpdateTemplateChildFunctionCall(targetElement, valueObjectName);

        return codeFormatting.Indent(updateByTemplateCall.Generate(elementTemplateObjectName.Name, elementTemplateObjectName.AppendToName, toChildQueryFunctionCall, updateTemplateChildFunctionCall));
    }

    private string GenerateToChildQueryFunctionCall(FromTemplateElementSelector toChildSelector)
    {
        const string templateContentAsParentName = "templateContent";

        return $"({templateContentAsParentName}) => " +
            toChildSelector.SelectionBy switch
            {
                FromTemplateElementSelection.Id => getElementByIdAsArrayCall.Generate(templateContentAsParentName, toChildSelector.Value),
                FromTemplateElementSelection.QuerySelector => getElementsByQuerySelectorCall.Generate(templateContentAsParentName, toChildSelector.Value),
                _ => throw new NotImplementedException($"{toChildSelector.SelectionBy} not implemented in {nameof(GenerateToChildQueryFunctionCall)}"),
            };
    }

    private string GenerateUpdateTemplateChildFunctionCall(TargetElement toChildTargetElement, string valueObjectName)
    {
        const string targetTemplateChildElements = "targetTemplateChildElements";
        var fullValueObject = $"vm.{valueObjectName}";

        return $"({targetTemplateChildElements}) => " +
            toChildTargetElement.Place.ElementPlacing switch
            {
                ElementPlacing.Attribute => setElementsAttributeCall.Generate(targetTemplateChildElements, toChildTargetElement.Place.Value, fullValueObject),
                ElementPlacing.Content => setElementsContentCall.Generate(targetTemplateChildElements, fullValueObject),
                _ => throw new NotImplementedException($"{toChildTargetElement.Place.ElementPlacing} not implemented in {nameof(GenerateUpdateTemplateChildFunctionCall)}"),
            };
    }
}