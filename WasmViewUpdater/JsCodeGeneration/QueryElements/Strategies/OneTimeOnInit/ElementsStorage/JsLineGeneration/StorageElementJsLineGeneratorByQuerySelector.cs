using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByQuerySelector(IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling) : IStorageElementJsLineGeneratorByQuerySelector
{
    public string Generate(string elementObjectName, string querySelector, string parentObjectName)
        => $"{getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, querySelector, elementObjectName)};";
}
