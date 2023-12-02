namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementObjectSelector : ElementSelector
    {
        internal ElementObjectSelector(string objectName)
            : base(ElementSelection.ElementObject, objectName)
        {
        }
    }
}
