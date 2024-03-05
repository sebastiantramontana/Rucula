using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    public interface IAppendToTemplateSelectorBuilder
    {
        IToChildTemplateSelectorBuilder AppendTo(FromTemplateAppendToElementSelector selector);
    }
}
