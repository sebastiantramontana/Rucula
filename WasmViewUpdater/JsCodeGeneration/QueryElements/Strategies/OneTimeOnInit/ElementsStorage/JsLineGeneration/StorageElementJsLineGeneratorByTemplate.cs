using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate(
    IGetStoredElementByTemplateAsArrayCall getStoredElementByTemplateAsArrayCall,
    IStorageFromTemplateElementJsLineGenerator storageFromTemplateElementJsLineGenerator) 
    : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectToAppend)
    {
        var codeBuilder = new StringBuilder($"{getStoredElementByTemplateAsArrayCall.Generate(elementObjectName.AssociatedSelector.Value, elementObjectName.Name)};");

        var templateSelector = elementObjectName.AssociatedSelector as ElementTemplateSelector;

        codeBuilder
            .AppendLine()
            .Append($"{storageFromTemplateElementJsLineGenerator.Generate(templateSelector!.ElementToAppend, $"{elementObjectName.Name}_appendTo", parentObjectToAppend)};");

        return codeBuilder.ToString();
    }
}
