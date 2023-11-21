using Moq;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Test.Modeling
{
    internal static class TestHelper
    {
        public static ElementSelector CreateSelector(ElementSelection selectionBy, string value, string parent)
            => new(selectionBy, value, parent);

        public static ValueModel CreateValueModel(Delegate valueFunc, IEnumerable<(ElementSelector Selector, ElementPlace Place)> targetElementsData)
        {
            var valueModel = new ValueModel(valueFunc);
            valueModel.TargetElements = targetElementsData.Select((data) => CreateTargetElement(data.Selector, valueModel, data.Place)).ToArray();

            return valueModel;
        }

        public static TargetElement CreateTargetElement(ElementSelector selector, ValueModel valueModel, ElementPlace place)
            => new(selector, valueModel) { Place = place };

        public static ElementPlace CreateAttributeElementPlace(string value)
            => new(ElementPlacing.Attribute, value);

        public static ElementPlace CreateContentElementPlace()
            => new(ElementPlacing.Content);

        public static void AssertValueModel(ValueModel actualValueModel, ValueModel expectedValueModel, bool canPlaceBeNull)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualValueModel.ValueFunc, Is.EqualTo(expectedValueModel.ValueFunc));
                Assert.That(actualValueModel.TargetElements.Count(), Is.EqualTo(expectedValueModel.TargetElements.Count()));
            });

            for (int i = 0; i < expectedValueModel.TargetElements.Count(); i++)
            {
                AssertTargetElement(actualValueModel.TargetElements.ElementAt(i), expectedValueModel.TargetElements.ElementAt(i), canPlaceBeNull);
            }
        }

        public static void AssertTargetElement(TargetElement actualTargetElement, TargetElement expectedTargetElement, bool canPlaceBeNull)
        {
            AssertPlace(actualTargetElement.Place, expectedTargetElement.Place, canPlaceBeNull);
            AssertSelector(actualTargetElement.Selector, expectedTargetElement.Selector);
            Assert.That(actualTargetElement.Parent.ValueFunc, Is.EqualTo(expectedTargetElement.Parent.ValueFunc));
        }

        public static void AssertPlace(ElementPlace actualPlace, ElementPlace expectedPlace, bool canPlaceBeNull)
        {
            if (canPlaceBeNull)
                Assert.That(actualPlace, Is.Null.And.EqualTo(expectedPlace).Or.Not.Null.And.EqualTo(expectedPlace));
            else
                Assert.That(actualPlace, Is.EqualTo(expectedPlace).And.Not.Null);
        }

        public static void AssertSelector(ElementSelector actualSelector, ElementSelector expectedSelector)
        {
            Assert.That(actualSelector, Is.EqualTo(expectedSelector));
        }
    }
}
