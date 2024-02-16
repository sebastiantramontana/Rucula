namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByQuerySelector
{
    string Generate(string elementObjectName, string querySelector, string parentObjectName);
}
