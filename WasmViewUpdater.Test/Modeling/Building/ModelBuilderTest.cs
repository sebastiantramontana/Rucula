using Moq;
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

            var selectorMock1 = TestHelper.CreateSelector(ElementSelection.Id, "test-id", "document");
            var selectorMock2 = TestHelper.CreateSelector(ElementSelection.QuerySelector, ".test > p", "document");
            var selectorMock3 = TestHelper.CreateSelector(ElementSelection.Template, "template-id", "parent-element-id");

            var func1 = (EntityTest e) => e.Name;
            var func2 = (EntityTest e) => e.Age;

            var expectedValue1 = CreateExpectedValue1();
            var expectedValue2 = CreateExpectedValue2();

            var result1 = sut
                .Value(func1)
                .ToContainerElement(selectorMock1).ToContent()
                .ToElement(selectorMock2).ToAttribute("data-name")
                .ToContainerElement(selectorMock3).ToContent();

            var result2 = sut
                .Value(func2)
                .ToElement(selectorMock2).ToAttribute("data-age");

            Assert.That(sut.Values.Count(), Is.EqualTo(2));
            TestHelper.AssertValueModel(sut.Values.ElementAt(0), expectedValue1);
            TestHelper.AssertValueModel(sut.Values.ElementAt(1), expectedValue2);

            ValueModel CreateExpectedValue1()
                => TestHelper.CreateValueModel(func1,
                    [
                        (selectorMock1, TestHelper.CreateContentElementPlace()),
                        (selectorMock2, TestHelper.CreateAttributeElementPlace("data-name")),
                        (selectorMock3, TestHelper.CreateContentElementPlace())
                    ]);

            ValueModel CreateExpectedValue2()
                => TestHelper.CreateValueModel(func2,
                    [
                        (selectorMock2, TestHelper.CreateAttributeElementPlace("data-age"))
                    ]);
        }
    }
}
