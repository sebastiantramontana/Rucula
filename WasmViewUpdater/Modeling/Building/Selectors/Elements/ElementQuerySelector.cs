namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementQuerySelector : ElementSelector
    {
        internal ElementQuerySelector(string querySelector, ElementSelector? parent)
            : base(ElementSelection.QuerySelector, querySelector, parent)
        {
        }
    }
}
