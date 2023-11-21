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

            var selectorMock1 = TestHelper.MockSelector(ElementSelection.Id, "test-id", "document");
            var selectorMock2 = TestHelper.MockSelector(ElementSelection.Id, ".test > p", "document");
            var selectorMock3 = TestHelper.MockSelector(ElementSelection.Id, "template-id", "parent-element-id");

            var func1 = (EntityTest e) => e.Name;
            var func2 = (EntityTest e) => e.Age;

            var expectedValue1 = CreateExpectedValue1();
            var expectedValue2 = CreateExpectedValue2();

            var result1 = sut
                .Value(func1)
                .ToContainerElement(selectorMock1.Object).ToContent()
                .ToElement(selectorMock2.Object).ToAttribute("data-name")
                .ToContainerElement(selectorMock3.Object).ToContent();

            var result2 = sut
                .Value(func2)
                .ToElement(selectorMock2.Object).ToAttribute("data-age");

            Assert.That(sut.Values.Count(), Is.EqualTo(2));
            TestHelper.AssertValueModel(sut.Values.ElementAt(0), expectedValue1);
            TestHelper.AssertValueModel(sut.Values.ElementAt(1), expectedValue2);

            ValueModel CreateExpectedValue1()
                => TestHelper.CreateValueModel(func1,
                    [
                        (selectorMock1.Object, TestHelper.CreateContentElementPlace()),
                        (selectorMock2.Object, TestHelper.CreateAttributeElementPlace("data-name")),
                        (selectorMock3.Object, TestHelper.CreateContentElementPlace())
                    ]);

            ValueModel CreateExpectedValue2()
                => TestHelper.CreateValueModel(func2,
                    [
                        (selectorMock2.Object, TestHelper.CreateAttributeElementPlace("data-age"))
                    ]);
        }
    }
}
