namespace Vitraux.Modeling.Building.Elements
{

    public interface IElementAttributeBuilder<TViewModel>
    {
        IFinalizableBuilder<TViewModel> ToAttribute(string attribute);
    }
}
