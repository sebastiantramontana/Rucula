using Vitraux.Modeling;
using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Modeling.Models;
using Vitraux.Test.Example;
using Vitraux.Test.Modeling;

namespace Vitraux.Test;

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
                    (new ElementIdSelector("another-anchor-cell-mascota-nombre-id"),TestHelper.CreateAttributeElementPlace("href")),
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

        sut.Configure(modelBuilder, elementSelectorBuilder, rowSelectorBuilder);

        var data = modelBuilder as IModelBuilderData;

        TestHelper.AssertValueModel(data.Values.ElementAt(0), value1, false);
        TestHelper.AssertValueModel(data.Values.ElementAt(1), value2, false);
        TestHelper.AssertCollectionTableModel(data.CollectionTables.ElementAt(0), collection);
    }
}
