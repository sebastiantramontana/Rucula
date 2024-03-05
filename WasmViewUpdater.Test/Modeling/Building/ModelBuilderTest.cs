using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ModelBuilderTest
    {
        [Test]
        public void BuildModelValuesTest()
        {
            IModelBuilder<ViewModelTest> sut = new InitialModelBuilder<ViewModelTest>();

            var selector1 = new ElementIdSelector("test-id");
            var selector2 = new ElementQuerySelector(".test > p");
            var selector3 = new ElementTemplateSelector("template-id", new FromTemplateAppendToElementIdSelector("element-to-append-id"));

            var func1 = (ViewModelTest e) => e.Name;
            var func2 = (ViewModelTest e) => e.Age;

            var expectedValue1 = CreateExpectedValue1();
            var expectedValue2 = CreateExpectedValue2();

            var result1 = sut
                .Value(func1)
                .ToContainerElement(selector1).ToContent()
                .ToElement(selector2).ToAttribute("data-name")
                .ToContainerElement(selector3).ToContent();

            var result2 = sut
                .Value(func2)
                .ToElement(selector2).ToAttribute("data-age");

            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);

            Assert.That(sut.Values.Count(), Is.EqualTo(2));
            TestHelper.AssertValueModel(sut.Values.ElementAt(0), expectedValue1, false);
            TestHelper.AssertValueModel(sut.Values.ElementAt(1), expectedValue2, false);

            ValueModel CreateExpectedValue1()
                => TestHelper.CreateValueModel(func1,
                    [
                        TestHelper.CreateTargetElement(selector1, TestHelper.CreateContentElementPlace()),
                        TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                        TestHelper.CreateTargetElement(selector3, TestHelper.CreateContentElementPlace())
                    ]);

            ValueModel CreateExpectedValue2()
                => TestHelper.CreateValueModel(func2,
                    [
                        TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-age"))
                    ]);
        }
    }
}
