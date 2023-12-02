using NUnit.Framework.Constraints;
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

        var collection = TestHelper.CreateCollectionTableModel(
            (Persona p) => p.Mascotas,
            new ElementIdSelector("mascotas-table-id"),
            new TemplateRowSelector("row-template-id"),
            new[]
            {
                TestHelper.CreateValueModel((Mascota m) => m.Name, new (ElementSelector, ElementPlace)[]
                {
                    (new ElementIdSelector("cell-mascota-nombre-id"),TestHelper.CreateContentElementPlace()),
                    (new ElementIdSelector("anchor-cell-mascota-nombre-id"),TestHelper.CreateAttributeElementPlace("href")),
                    (new ElementIdSelector("another-anchor-cell-mascota-nombre-id"),TestHelper.CreateContentElementPlace()),
                }),
                TestHelper.CreateValueModel((Mascota m) => m.IsDespulgado, new (ElementSelector, ElementPlace)[]
                {
                    (new ElementIdSelector("some-despulgado-id"),TestHelper.CreateAttributeElementPlace("data-despulgado")),
                })
            },
            new[]
            {
                TestHelper.CreateCollectionTableModel(
                (Mascota m) => m.Vacunas,
                new ElementIdSelector("inner-table-vacunas"),
                new TemplateRowSelector("row-template-vacunas-id"),
                new[]
                {
                    TestHelper.CreateValueModel((Vacuna v) => v.Name, new (ElementSelector, ElementPlace)[]
                    {
                        (new ElementIdSelector("div-vacuna-id"),TestHelper.CreateContentElementPlace())
                    }),
                    TestHelper.CreateValueModel((Vacuna v) => v.DateApplied, new (ElementSelector, ElementPlace)[]
                    {
                        (new ElementIdSelector("span-vacuna-id"),TestHelper.CreateContentElementPlace())
                    })
                },
                Enumerable.Empty<CollectionTableModel>())
            });


        /*
         * modelBuilder
            .Collection(a => a.Mascotas)
            .ToTable(ById("mascotas-table-id"))
            .FillRows(RowFromTemplate("row-template-id"))
                .Value(m => m.Name)
                    .ToContainerElement(ById("cell-mascota-nombre-id")).ToContent()
                    .ToElement(ById("anchor-cell-mascota-nombre-id")).ToAttribute("href")
                    .ToContainerElement(ById("another-anchor-cell-mascota-nombre-id")).ToAttribute("href")
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
        TestHelper.AssertCollectionTableModel(newModelBuilder.CollectionTables.ElementAt(0), collection);
    }


}
