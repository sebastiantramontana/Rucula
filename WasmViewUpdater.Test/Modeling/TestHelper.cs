using Moq;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Test.Modeling
{
    internal static class TestHelper
    {
        public static Mock<IElementSelector> MockSelector(ElementSelection selection, string value, string parent)
        {
            var selectorMock = new Mock<IElementSelector>();
            selectorMock.Setup(s => s.SelectionBy).Returns(selection);
            selectorMock.Setup(s => s.Value).Returns(value);
            selectorMock.Setup(s => s.Parent).Returns(parent);

            return selectorMock;
        }

        public static ValueModel CreateValueModel(Delegate valueFunc, IEnumerable<(IElementSelector Selector, ElementPlace Place)> targetElementsData)
        {
            var valueModel = new ValueModel(valueFunc);
            valueModel.TargetElements = targetElementsData.Select((data) => CreateTargetElement(data.Selector, valueModel, data.Place)).ToArray();

            return valueModel;
        }

        public static TargetElement CreateTargetElement(IElementSelector selector, ValueModel valueModel, ElementPlace place)
            => new(selector, valueModel) { Place = place };

        public static ElementPlace CreateAttributeElementPlace(string value)
            => new(ElementPlacing.Attribute, value);

        public static ElementPlace CreateContentElementPlace()
            => new(ElementPlacing.Content);

        public static void AssertValueModel(ValueModel actualValueModel, ValueModel expectedValueModel)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualValueModel.ValueFunc, Is.EqualTo(expectedValueModel.ValueFunc));
                Assert.That(actualValueModel.TargetElements.Count(), Is.EqualTo(expectedValueModel.TargetElements.Count()));
            });

            for (int i = 0; i < expectedValueModel.TargetElements.Count(); i++)
            {
                AssertTargetElement(actualValueModel.TargetElements.ElementAt(i), expectedValueModel.TargetElements.ElementAt(i));
            }
        }

        public static void AssertTargetElement(TargetElement actualTargetElement, TargetElement expectedTargetElement)
        {
            AssertPlace(actualTargetElement.Place, expectedTargetElement.Place);
            AssertSelector(actualTargetElement.Selector, expectedTargetElement.Selector);
            Assert.That(actualTargetElement.Parent.ValueFunc, Is.EqualTo(expectedTargetElement.Parent.ValueFunc));
        }

        public static void AssertPlace(ElementPlace actualPlace, ElementPlace expectedPlace)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualPlace.ElementPlacing, Is.EqualTo(expectedPlace.ElementPlacing));
                Assert.That(actualPlace.Value, Is.EqualTo(expectedPlace.Value));
            });
        }

        public static void AssertSelector(IElementSelector actualSelector, IElementSelector expectedSelector)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualSelector.SelectionBy, Is.EqualTo(expectedSelector.SelectionBy));
                Assert.That(actualSelector.Parent, Is.EqualTo(expectedSelector.Parent));
                Assert.That(actualSelector.Value, Is.EqualTo(expectedSelector.Value));
            });
        }

    }
}
