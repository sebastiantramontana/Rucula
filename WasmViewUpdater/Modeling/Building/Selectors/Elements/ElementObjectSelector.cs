namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementObjectSelector : ElementSelector
    {
        internal ElementObjectSelector(string objectName, ElementSelector? parent)
            : base(ElementSelection.ElementObject, objectName, parent)
        {
        }
    }
}
