using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling.Building.Elements
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ElementPlaceBuildingTest
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

        private void TestPlace(ElementPlace expectedPlace, Func<IELementContentBuilder<ViewModelTest>, IFinalizableBuilder<ViewModelTest>> actFunc)
        {
            var valueModel = new ValueModel((ViewModelTest e) => e.Weigth);
            var modelBuilder = new ModelBuilder<ViewModelTest>();
            var valueModelBuilder = new ValueModelBuilder<ViewModelTest>(valueModel, modelBuilder);
            var selector = new ElementTemplateSelector("template-id", new FromTemplateElementIdSelector("append-to-element-id"));
            var sut = valueModelBuilder.ToContainerElement(selector);

            var result = actFunc(sut);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertPlace(valueModel.TargetElements.Single().Place, expectedPlace, false);
        }
    }
}
