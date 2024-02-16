namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorById
{
    string Generate(string elementObjectName, string id, string parentObjectName);
}
