namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    public interface IAppendToTemplateSelectorBuilder
    {
        IToChildTemplateSelectorBuilder AppendTo(ElementSelector selector);
    }
}
