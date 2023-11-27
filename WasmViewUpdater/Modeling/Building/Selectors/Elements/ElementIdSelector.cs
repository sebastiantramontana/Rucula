namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementIdSelector : ElementSelector
    {
        internal ElementIdSelector(string elementId, ElementSelector? parent)
            : base(ElementSelection.Id, elementId, parent)
        {
        }
    }
}
