using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorById(IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling) : IStorageElementJsLineGeneratorById
{
    public string Generate(string elementObjectName, string id, string parentObjectName)
        => $"{getStoredElementByIdAsArrayCalling.Generate(parentObjectName, id, elementObjectName)};";
}
