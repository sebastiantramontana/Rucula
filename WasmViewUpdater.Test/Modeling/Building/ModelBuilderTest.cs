using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ModelBuilderTest
    {
        [Test]
        public void BuildModelValuesTest()
        {
            IModelBuilder<EntityTest> sut = new ModelBuilder<EntityTest>();

            var selector1 = TestHelper.CreateSelector(ElementSelection.Id, "test-id", "document");
            var selector2 = TestHelper.CreateSelector(ElementSelection.QuerySelector, ".test > p", "document");
            var selector3 = TestHelper.CreateSelector(ElementSelection.Template, "template-id", "parent-element-id");

            var func1 = (EntityTest e) => e.Name;
            var func2 = (EntityTest e) => e.Age;

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
                        (selector1, TestHelper.CreateContentElementPlace()),
                        (selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                        (selector3, TestHelper.CreateContentElementPlace())
                    ]);

            ValueModel CreateExpectedValue2()
                => TestHelper.CreateValueModel(func2,
                    [
                        (selector2, TestHelper.CreateAttributeElementPlace("data-age"))
                    ]);
        }
    }
}
