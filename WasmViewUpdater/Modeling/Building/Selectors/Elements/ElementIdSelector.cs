namespace Vitraux.Modeling.Building.Selectors.Elements
{
    internal record class ElementIdSelector : ElementSelector
    {
        internal ElementIdSelector(string elementId)
            : base(ElementSelection.Id, elementId)
        {
        }
    }
}
