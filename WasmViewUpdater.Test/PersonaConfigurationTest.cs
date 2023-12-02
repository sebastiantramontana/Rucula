using WasmViewUpdater.Modeling;
using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;
using WasmViewUpdater.Modeling.Models;
using WasmViewUpdater.Test.Example;
using WasmViewUpdater.Test.Modeling;

namespace WasmViewUpdater.Test;

[TestFixture]
internal class PersonaConfigurationTest
{
    [Test]
    public void Test()
    {
        IModelConfiguration<Persona> sut = new PersonaConfiguration();
        var modelBuilder = new ModelBuilder<Persona>();
        var elementSelectorBuilder = new ElementSelectorBuilder();
        var rowSelectorBuilder = new RowSelectorBuilder();

        var value1 = TestHelper.CreateValueModel((Persona p) => p.Name, new (ElementSelector, ElementPlace)[]
        {
            (new ElementIdSelector("algo-name"),TestHelper.CreateAttributeElementPlace("alt"))
        });

        var templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("otro-template-id", "parent-to-add-id", "child-target-id");

        var value2 = TestHelper.CreateValueModel((Persona p) => p.Edad, new (ElementSelector, ElementPlace)[]
        {
            (templateForValue2, TestHelper.CreateContentElementPlace()),
            (new ElementQuerySelector(".p-otro > img"), TestHelper.CreateAttributeElementPlace("data-otro"))
        });

        //var tableRowSelector = new TemplateRowSelector("row-template-id");

        //var collection = TestHelper.CreateCollectionTableModel(
        //    (Persona p) => p.Mascotas,
        //    new ElementIdSelector("mascotas-id", ElementSelector.DocumentElement),
        //    tableRowSelector,
        //    TestHelper.CreateValueModel((Mascota m) => m.Name,new (ElementSelector, ElementPlace)[]
        //    {
        //        (new ElementIdSelector("cell-mascota-nombre-id",tableRowSelector),TestHelper.CreateContentElementPlace())
        //    }),
        //    new[]
        //    {

        //    }


        /*
         * modelBuilder
            .Collection(a => a.Mascotas)
            .ToTable(ById("mascotas-id"))
            .FillRows(RowFromTemplate("row-template-id"))
                .Value(m => m.Name)
                    .ToContainerElement(ById("cell-mascota-nombre-id")).ToContent()
                    .ToElement(ById("anchor--cell-mascota-nombre-id")).ToAttribute("href")
                    .ToContainerElement(ById("another-anchor--cell-mascota-nombre-id")).ToAttribute("href")
                .Value(m => m.IsDespulgado)
                    .ToElement(ById("some-despulgado-id")).ToAttribute("data-despulgado")
                .Collection(m => m.Vacunas)
                .ToTable(ById("inner-table-vacunas"))
                .FillRows(RowFromTemplate("row-template-vacunas-id"))
                    .Value(v => v.Name).ToContainerElement(ById("div-vacuna-id")).ToContent()
                    .Value(v => v.DateApplied).ToContainerElement(ById("span-vacuna-id")).ToContent();
         * */

        var newModelBuilder = sut.Configure(modelBuilder, elementSelectorBuilder, rowSelectorBuilder);

        TestHelper.AssertValueModel(newModelBuilder.Values.ElementAt(0), value1, false);
        TestHelper.AssertValueModel(newModelBuilder.Values.ElementAt(1), value2, false);
    }


}
