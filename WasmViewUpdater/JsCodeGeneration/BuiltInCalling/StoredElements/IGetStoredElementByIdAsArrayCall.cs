namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal interface IGetStoredElementByIdAsArrayCall
    {
        string Generate(string parentObjectName, string id, string elementObjectName);
    }
}