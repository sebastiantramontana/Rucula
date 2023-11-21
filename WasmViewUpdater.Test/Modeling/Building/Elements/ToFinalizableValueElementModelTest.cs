using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ToFinalizableValueElementModelTest
    {
        [Test]
        public void ContentPlaceTest()
        {
            var expectedPlace = TestHelper.CreateContentElementPlace();
            TestPlace(expectedPlace, sut => sut.ToContent());
        }

        [Test]
        public void AttributePlaceTest()
        {
            var expectedPlace = TestHelper.CreateAttributeElementPlace("data-weight");
            TestPlace(expectedPlace, sut => sut.ToAttribute("data-weight"));
        }

        private void TestPlace(ElementPlace expectedPlace, Func<ToFinalizableValueElementModel, IFinalizableValueModel> actFunc)
        {
            var selector = TestHelper.CreateSelector(ElementSelection.Template, "template-id", "parent-id");
            var valueModel = TestHelper.CreateValueModel((EntityTest e) => e.Weigth, [(selector, default!)]);
            var actualTargetElement = valueModel.TargetElements.Single();
            var sut = new ToFinalizableValueElementModel(actualTargetElement);

            var result = actFunc(sut);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertPlace(actualTargetElement.Place, expectedPlace, false);
        }
    }
}
