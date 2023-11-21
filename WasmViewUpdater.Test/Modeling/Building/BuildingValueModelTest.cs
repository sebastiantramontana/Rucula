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
            TestAddingNewTargetValueToValueModel<IToContainerElementModel<IFinalizableValueModel>>((sut) => sut.ToContainerElement);
        }

        [Test]
        public void ToElementTest()
        {
            TestAddingNewTargetValueToValueModel<IToElementModel<IFinalizableValueModel>>((sut) => sut.ToElement);
        }

        private void TestAddingNewTargetValueToValueModel<TReturn>(Func<BuildingValueModel, Func<ElementSelector, TReturn>> getActFunc)
            where TReturn : IToElementModel<IFinalizableValueModel>
        {
            var selector1 = TestHelper.CreateSelector(ElementSelection.Id, "test-id", "document");
            var selector2 = TestHelper.CreateSelector(ElementSelection.QuerySelector, ".test > p", "document");
            var selector3 = TestHelper.CreateSelector(ElementSelection.Template, "template-id", "parent-element-id");

            var func1 = (EntityTest e) => e.Name;

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

            var sut = new BuildingValueModel(actualvalue);

            var result = getActFunc(sut).Invoke(selector3);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertValueModel(actualvalue, expectedValue, true);
        }
    }
}
