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
public class PetOwnerConfigurationTest
{
    private readonly IDataUriConverter dataUriConverter = new DataUriConverter();

    [Test]
    public void Test()
    {
        IModelConfiguration<PetOwner> sut = new PetOwnerConfiguration(dataUriConverter);
        var modelBuilder = new InitialModelBuilder<PetOwner>();
        var elementSelectorBuilder = new ElementSelectorBuilder();
        var rowSelectorBuilder = new RowSelectorBuilder();
        var fromTemplateElementSelectorBuilder = new FromTemplateAppendToElementSelectorBuilder();

        var value1 = TestHelper.CreateValueModel((PetOwner a) => a.Name,
        [
            TestHelper.CreateTargetElement(new ElementIdSelector("petowner-name"), TestHelper.CreateContentElementPlace())
        ]);

        var templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("petowner-address-template", "petowner-parent", ".petowner-address-target");

        var value2 = TestHelper.CreateValueModel((PetOwner a) => a.Address,
        [
            TestHelper.CreateTargetElement(templateForValue2, TestHelper.CreateContentElementPlace()),
            TestHelper.CreateTargetElement(new ElementQuerySelector(".petwoner-address > div"), TestHelper.CreateAttributeElementPlace("data-petowner-address"))
        ]);

        var collection = TestHelper.CreateCollectionTableModel(
            (PetOwner a) => a.pets,
            new ElementIdSelector("pets-table"),
            new TemplateRowSelector("pet-row-template"),
            [
                TestHelper.CreateValueModel((Pet m) => m.Name,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".cell-pet-name"),TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".another-anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                ]),
                TestHelper.CreateValueModel((Pet m) => ToDataUri(m.Photo),
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".pet-photo"), TestHelper.CreateAttributeElementPlace("src")),
                ])
            ],
            [
                TestHelper.CreateCollectionTableModel(
                (Pet m) => m.Vaccines,
                new ElementIdSelector("inner-table-vaccines"),
                new TemplateRowSelector("row-template-vaccines"),
                [
                    TestHelper.CreateValueModel((Vaccine v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".div-vaccine"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Vaccine v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".span-vaccine-date"),TestHelper.CreateContentElementPlace())
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

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
