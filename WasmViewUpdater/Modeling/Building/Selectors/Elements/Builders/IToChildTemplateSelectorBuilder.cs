namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    public interface IToChildTemplateSelectorBuilder
    {
        ElementSelector ToChild(ElementQuerySelector selector);
    }
}
