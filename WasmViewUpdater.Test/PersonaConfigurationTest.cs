using Vitraux.Helpers;
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
        IModelConfiguration<PetOwner> sut = new PetOwnerConfiguration(new DataUriConverter());
        var modelBuilder = new InitialModelBuilder<PetOwner>();
        var elementSelectorBuilder = new ElementSelectorBuilder();
        var rowSelectorBuilder = new RowSelectorBuilder();
        var fromTemplateElementSelectorBuilder = new FromTemplateAppendToElementSelectorBuilder();

        var value1 = TestHelper.CreateValueModel((PetOwner p) => p.Name,
        [
            TestHelper.CreateTargetElement(new ElementIdSelector("algo-name"), TestHelper.CreateAttributeElementPlace("alt"))
        ]);

        var templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("otro-template-id", "parent-to-add-id", ".child-target");

        var value2 = TestHelper.CreateValueModel((PetOwner p) => p.Edad,
        [
            TestHelper.CreateTargetElement(templateForValue2, TestHelper.CreateContentElementPlace()),
            TestHelper.CreateTargetElement(new ElementQuerySelector(".p-otro > img"), TestHelper.CreateAttributeElementPlace("data-otro"))
        ]);

        var collection = TestHelper.CreateCollectionTableModel(
            (PetOwner p) => p.Mascotas,
            new ElementIdSelector("mascotas-table-id"),
            new TemplateRowSelector("row-template-id"),
            [
                TestHelper.CreateValueModel((Pet m) => m.Name,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".cell-mascota-nombre"),TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".anchor-cell-mascota-nombre"),TestHelper.CreateAttributeElementPlace("href")),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".another-anchor-cell-mascota-nombre"),TestHelper.CreateAttributeElementPlace("href")),
                ]),
                TestHelper.CreateValueModel((Pet m) => m.IsDespulgado,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".some-despulgado"),TestHelper.CreateAttributeElementPlace("data-despulgado")),
                ])
            ],
            [
                TestHelper.CreateCollectionTableModel(
                (Pet m) => m.Vacunas,
                new ElementIdSelector("inner-table-vacunas"),
                new TemplateRowSelector("row-template-vacunas-id"),
                [
                    TestHelper.CreateValueModel((Vaccine v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".div-vacuna"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Vaccine v) => v.DateApplied,
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
