using Vitraux.Helpers;
using Vitraux.Modeling;
using Vitraux.Modeling.Building;

namespace Vitraux.Test.Example;

public class PetOwnerConfiguration(IDataUriConverter dataUriConverter) : ModelConfigurationBase<PetOwner>
{
    protected override void Configure(IModelBuilder<PetOwner> modelBuilder)
    {
        modelBuilder
            .Value(a => a.Name)
                .ToContainerElement(ById("petowner-name"))
                .ToContent()
            .Value(a => a.Address)
                .ToContainerElement(
                        FromTemplate("petowner-address-template")
                            .AppendTo(DocumentById("petowner-parent"))
                            .ToChild(ByQuerySelector(".petowner-address-target")))
                .ToContent()
                .ToElement(ByQuerySelector(".petwoner-address > div"))
                    .ToAttribute("data-petowner-address")
            .Collection(a => a.pets)
                .ToTable(ById("pets-table"))
                .RowsFrom(Template("pet-row-template"))
                    .Value(m => m.Name)
                        .ToContainerElement(ByQuerySelector(".cell-pet-name")).ToContent()
                        .ToElement(ByQuerySelector(".anchor-cell-pet-name")).ToAttribute("href")
                        .ToContainerElement(ByQuerySelector(".another-anchor-cell-pet-name")).ToAttribute("href")
                    .Value(m => ToDataUri(m.Photo))
                        .ToElement(ByQuerySelector(".pet-photo")).ToAttribute("src")
                    .Collection(m => m.Vaccines)
                        .ToTable(ById("inner-table-vaccines"))
                        .RowsFrom(Template("row-template-vaccines"))
                            .Value(v => v.Name).ToContainerElement(ByQuerySelector(".div-vaccine")).ToContent()
                            .Value(v => v.DateApplied).ToContainerElement(ByQuerySelector(".span-vaccine-date")).ToContent();
    }

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
