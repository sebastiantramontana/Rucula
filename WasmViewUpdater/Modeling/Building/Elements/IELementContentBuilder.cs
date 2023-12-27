namespace Vitraux.Modeling.Building.Elements
{
    public interface IELementContentBuilder<TViewModel> : IElementAttributeBuilder<TViewModel>
    {
        IFinalizableBuilder<TViewModel> ToContent();
    }
}
