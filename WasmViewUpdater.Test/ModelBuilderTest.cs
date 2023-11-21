using Moq;
using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Test
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ModelBuilderTest
    {
        [Test]
        public void BuildModel()
        {
            IModelBuilder<EntityTest> sut = new ModelBuilder<EntityTest>();

            var selectorMock1 = new Mock<IElementSelector>();
            selectorMock1.Setup(s => s.SelectionBy).Returns(ElementSelection.Id);
            selectorMock1.Setup(s => s.Value).Returns("test-id");
            selectorMock1.Setup(s => s.Parent).Returns("document");

            var selectorMock2 = new Mock<IElementSelector>();
            selectorMock2.Setup(s => s.SelectionBy).Returns(ElementSelection.QuerySelector);
            selectorMock2.Setup(s => s.Value).Returns(".test > p");
            selectorMock2.Setup(s => s.Parent).Returns("document");

            var func1 = (EntityTest e) => e.Name;

            var result1 = sut
                .Value(func1)
                .ToContainerElement(selectorMock1.Object).ToContent()
                .ToElement(selectorMock2.Object).ToAttribute("data-name");

            var func2 = (EntityTest e) => e.Age;

            var result2 = sut
                .Value(func2)
                .ToElement(selectorMock2.Object).ToAttribute("data-age");

            Assert.That(sut.Values.Count(), Is.EqualTo(2));


            var expectedValue1 = CreateValueModel(func1,
                [
                    (selectorMock1.Object, CreateElementPlace(ElementPlacing.Content, string.Empty)),
                    (selectorMock2.Object, CreateElementPlace(ElementPlacing.Attribute, "data-name"))
                ]);

            var expectedValue2 = CreateValueModel(func2,
                [
                    (selectorMock2.Object, CreateElementPlace(ElementPlacing.Attribute, "data-age"))
                ]);

            AssertValueModel(sut.Values.ElementAt(0), expectedValue1);
            AssertValueModel(sut.Values.ElementAt(1), expectedValue2);
        }

        private static ValueModel CreateValueModel(Delegate valueFunc, IEnumerable<(IElementSelector Selector, ElementPlace Place)> targetElementsData)
        {
            var valueModel = new ValueModel(valueFunc);
            valueModel.TargetElements = targetElementsData.Select((data) => CreateTargetElement(data.Selector, valueModel, data.Place));

            return valueModel;
        }

        private static TargetElement CreateTargetElement(IElementSelector selector, ValueModel valueModel, ElementPlace place)
            => new(selector, valueModel) { Place = place };

        private static ElementPlace CreateElementPlace(ElementPlacing placing, string value)
            => new(placing, value);

        private void AssertValueModel(ValueModel actualValueModel, ValueModel expectedValueModel)
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

        private void AssertTargetElement(TargetElement actualTargetElement, TargetElement expectedTargetElement)
        {
            AssertPlace(actualTargetElement.Place, expectedTargetElement.Place);
            AssertSelector(actualTargetElement.Selector, expectedTargetElement.Selector);
            Assert.That(actualTargetElement.Parent, Is.EqualTo(expectedTargetElement.Parent));
        }

        private static void AssertPlace(ElementPlace actualPlace, ElementPlace expectedPlace)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualPlace.ElementPlacing, Is.EqualTo(expectedPlace.ElementPlacing));
                Assert.That(actualPlace.Value, Is.EqualTo(expectedPlace.Value));
            });
        }

        private static void AssertSelector(IElementSelector actualSelector, IElementSelector expectedSelector)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualSelector.SelectionBy, Is.EqualTo(expectedSelector.SelectionBy));
                Assert.That(actualSelector.Parent, Is.EqualTo(expectedSelector.Parent));
                Assert.That(actualSelector.Value, Is.EqualTo(expectedSelector.Value));
            });
        }
    }

    public class EntityTest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool Authorized { get; set; }
        public double Weigth { get; set; }
    }
}
