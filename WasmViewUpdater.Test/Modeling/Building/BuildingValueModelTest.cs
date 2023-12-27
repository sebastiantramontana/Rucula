using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;
using WasmViewUpdater.Test.Example;

namespace WasmViewUpdater.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class BuildingValueModelTest
    {
        [Test]
        public void ToContainerElementTest()
        {
            TestAddingNewTargetValueToValueModel<Persona, IELementContentBuilder<Persona>>((sut) => sut.ToContainerElement);
        }

        [Test]
        public void ToElementTest()
        {
            TestAddingNewTargetValueToValueModel<Persona, IElementAttributeBuilder<Persona>>((sut) => sut.ToElement);
        }

        private void TestAddingNewTargetValueToValueModel<TViewModel, TReturn>(Func<ValueModelBuilder<TViewModel>, Func<ElementSelector, TReturn>> getActFunc)
            where TReturn : IElementAttributeBuilder<TViewModel>
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

            var modelBuilder = new ModelBuilder<TViewModel>();
            var sut = new ValueModelBuilder<TViewModel>(actualvalue, modelBuilder);

            var result = getActFunc(sut).Invoke(selector3);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertValueModel(actualvalue, expectedValue, true);
        }
    }
}
