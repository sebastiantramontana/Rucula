namespace WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders
{
    internal class ToChildTemplateSelectorBuilder : IToChildTemplateSelectorBuilder
    {
        private readonly ElementTemplateSelector _templateSelector;

        public ToChildTemplateSelectorBuilder(ElementTemplateSelector templateSelector)
        {
            _templateSelector = templateSelector;
        }

        public ElementSelector ToChild(ElementSelector childSelector)
        {
            _templateSelector.TargetChildElement = childSelector;
            return _templateSelector;
        }
    }
}
