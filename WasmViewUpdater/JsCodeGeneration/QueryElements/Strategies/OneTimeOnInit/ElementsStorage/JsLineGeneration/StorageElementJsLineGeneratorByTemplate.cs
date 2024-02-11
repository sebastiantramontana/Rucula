using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(ElementObjectName elementObjectName)
    {
        var codeBuilder = new StringBuilder($"globalThis.vitraux.storedElements.getStoredElementByTemplateAsArray('{elementObjectName.AssociatedSelector.Value}', '{elementObjectName.Name}');");

        if (elementObjectName.AssociatedSelector is ElementTemplateSelector)
        {
            codeBuilder
                .AppendLine()
                .Append($"globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'parent-to-add-id', 'elements1_appendTo');");
        }

        return codeBuilder.ToString();
    }
}
