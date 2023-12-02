namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementQuerySelector : ElementSelector
    {
        internal ElementQuerySelector(string querySelector)
            : base(ElementSelection.QuerySelector, querySelector)
        {
        }
    }
}
