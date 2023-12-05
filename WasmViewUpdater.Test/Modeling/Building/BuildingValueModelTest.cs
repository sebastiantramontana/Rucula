using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class BuildingValueModelTest
    {
        [Test]
        public void ToContainerElementTest()
        {
            TestAddingNewTargetValueToValueModel<IELementContentBuilder<IFinalizableElementBuilder>>((sut) => sut.ToContainerElement);
        }

        [Test]
        public void ToElementTest()
        {
            TestAddingNewTargetValueToValueModel<IElementAttributeBuilder<IFinalizableElementBuilder>>((sut) => sut.ToElement);
        }

        private void TestAddingNewTargetValueToValueModel<TReturn>(Func<ValueElementBuilder, Func<ElementSelector, TReturn>> getActFunc)
            where TReturn : IElementAttributeBuilder<IFinalizableElementBuilder>
        {
            var selector1 = new ElementIdSelector("test-id");
            var selector2 = new ElementQuerySelector(".test > p");
            var selector3 = new ElementTemplateSelector("template-id", new ElementIdSelector("element-to-append-id"));

            var func1 = (ViewModelTest e) => e.Name;

            var actualvalue = TestHelper.CreateValueModel(func1,
                [
                    (selector1, TestHelper.CreateContentElementPlace()),
                    (selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                ]);

            var expectedValue = TestHelper.CreateValueModel(func1,
                [
                    (selector1, TestHelper.CreateContentElementPlace()),
                    (selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                    (selector3, default!),
                ]);

            var sut = new ValueElementBuilder(actualvalue);

            var result = getActFunc(sut).Invoke(selector3);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertValueModel(actualvalue, expectedValue, true);
        }
    }
}
