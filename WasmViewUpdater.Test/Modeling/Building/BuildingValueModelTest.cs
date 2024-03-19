using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Test.Example;

namespace Vitraux.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class BuildingValueModelTest
    {
        [Test]
        public void ToContainerElementTest()
        {
            TestAddingNewTargetValueToValueModel<PetOwner, IELementContentBuilder<PetOwner, ElementSelector>>((sut) => sut.ToContainerElement);
        }

        [Test]
        public void ToElementTest()
        {
            TestAddingNewTargetValueToValueModel<PetOwner, IElementAttributeBuilder<PetOwner, ElementSelector>>((sut) => sut.ToElement);
        }

        private void TestAddingNewTargetValueToValueModel<TViewModel, TReturn>(Func<ValueModelBuilder<TViewModel, ElementSelector>, Func<ElementSelector, TReturn>> getActFunc)
            where TReturn : IElementAttributeBuilder<TViewModel, ElementSelector>
        {
            var selector1 = new ElementIdSelector("test-id");
            var selector2 = new ElementQuerySelector(".test > p");
            var selector3 = new ElementTemplateSelector("template-id", new FromTemplateAppendToElementIdSelector("element-to-append-id"));

            var func1 = (ViewModelTest e) => e.Name;

            var actualvalue = TestHelper.CreateValueModel(func1,
            [
                TestHelper.CreateTargetElement(selector1, TestHelper.CreateContentElementPlace()),
                TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-name"))
            ]);

            var expectedValue = TestHelper.CreateValueModel(func1,
            [
                TestHelper.CreateTargetElement(selector1, TestHelper.CreateContentElementPlace()),
                TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                TestHelper.CreateTargetElement(selector3, default!),
            ]);

            var modelBuilder = new InitialModelBuilder<TViewModel>();
            var sut = new ValueModelBuilder<TViewModel, ElementSelector>(actualvalue, modelBuilder);

            var result = getActFunc(sut).Invoke(selector3);

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertValueModel(actualvalue, expectedValue, true);
        }
    }
}
