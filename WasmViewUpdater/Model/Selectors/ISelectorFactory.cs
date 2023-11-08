namespace WasmViewUpdater.Model.Selectors
{
    public interface ISelectorFactory
    {
        ISelector ById(string id);
        IFromTemplateSelector FromTemplate(string templateId);
        ISelector ByQuerySelector(string querySelector);
    }

    public interface IFromTemplateSelector
    {
        ISelectorFactory AddTo(ISelector selector);
    }
}
