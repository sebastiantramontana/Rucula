using Vitraux.Modeling;
using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Test.Example;
using Vitraux.Test.Modeling;

namespace Vitraux.Test;

[TestFixture]
public class PersonaConfigurationTest
{
    [Test]
    public void Test()
    {
        IModelConfiguration<Persona> sut = new PersonaConfiguration();
        var modelBuilder = new InitialModelBuilder<Persona>();
        var elementSelectorBuilder = new ElementSelectorBuilder();
        var rowSelectorBuilder = new RowSelectorBuilder();
        var fromTemplateElementSelectorBuilder = new FromTemplateAppendToElementSelectorBuilder();

        var value1 = TestHelper.CreateValueModel((Persona p) => p.Name,
        [
            TestHelper.CreateTargetElement(new ElementIdSelector("algo-name"), TestHelper.CreateAttributeElementPlace("alt"))
        ]);

        var templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("otro-template-id", "parent-to-add-id", ".child-target");

        var value2 = TestHelper.CreateValueModel((Persona p) => p.Edad,
        [
            TestHelper.CreateTargetElement(templateForValue2, TestHelper.CreateContentElementPlace()),
            TestHelper.CreateTargetElement(new ElementQuerySelector(".p-otro > img"), TestHelper.CreateAttributeElementPlace("data-otro"))
        ]);

        var collection = TestHelper.CreateCollectionTableModel(
            (Persona p) => p.Mascotas,
            new ElementIdSelector("mascotas-table-id"),
            new TemplateRowSelector("row-template-id"),
            [
                TestHelper.CreateValueModel((Mascota m) => m.Name,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".cell-mascota-nombre"),TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".anchor-cell-mascota-nombre"),TestHelper.CreateAttributeElementPlace("href")),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".another-anchor-cell-mascota-nombre"),TestHelper.CreateAttributeElementPlace("href")),
                ]),
                TestHelper.CreateValueModel((Mascota m) => m.IsDespulgado,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".some-despulgado"),TestHelper.CreateAttributeElementPlace("data-despulgado")),
                ])
            ],
            [
                TestHelper.CreateCollectionTableModel(
                (Mascota m) => m.Vacunas,
                new ElementIdSelector("inner-table-vacunas"),
                new TemplateRowSelector("row-template-vacunas-id"),
                [
                    TestHelper.CreateValueModel((Vacuna v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".div-vacuna"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Vacuna v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".span-vacuna"),TestHelper.CreateContentElementPlace())
                    ])
                ],
                [])
            ]);

        sut.Configure(modelBuilder, elementSelectorBuilder, rowSelectorBuilder, fromTemplateElementSelectorBuilder);

        var data = modelBuilder as IModelBuilderData;

        TestHelper.AssertValueModel(data.Values.ElementAt(0), value1, false);
        TestHelper.AssertValueModel(data.Values.ElementAt(1), value2, false);
        TestHelper.AssertCollectionTableModel(data.CollectionTables.ElementAt(0), collection);
    }
}
