using Vitraux.Modeling;
using Vitraux.Modeling.Building;

namespace Vitraux.Test.Example;

public class PersonaConfiguration : ModelConfigurationBase<Persona>
{
    protected override void Configure(IModelBuilder<Persona> modelBuilder)
    {
        modelBuilder
            .Value(a => a.Name)
                .ToElement(ById("algo-name"))
                .ToAttribute("alt")
            .Value(a => a.Edad)
                .ToContainerElement(
                        FromTemplate("otro-template-id")
                            .AppendTo(DocumentById("parent-to-add-id"))
                            .ToChild(ByQuerySelector(".child-target")))
                    .ToContent()
                .ToElement(ByQuerySelector(".p-otro > img"))
                    .ToAttribute("data-otro")
            .Collection(a => a.Mascotas)
                .ToTable(ById("mascotas-table-id"))
                .RowsFrom(Template("row-template-id"))
                    .Value(m => m.Name)
                        .ToContainerElement(ByQuerySelector(".cell-mascota-nombre")).ToContent()
                        .ToElement(ByQuerySelector(".anchor-cell-mascota-nombre")).ToAttribute("href")
                        .ToContainerElement(ByQuerySelector(".another-anchor-cell-mascota-nombre")).ToAttribute("href")
                    .Value(m => m.IsDespulgado)
                        .ToElement(ByQuerySelector(".some-despulgado")).ToAttribute("data-despulgado")
                    .Collection(m => m.Vacunas)
                        .ToTable(ById("inner-table-vacunas"))
                        .RowsFrom(Template("row-template-vacunas-id"))
                            .Value(v => v.Name).ToContainerElement(ByQuerySelector(".div-vacuna")).ToContent()
                            .Value(v => v.DateApplied).ToContainerElement(ByQuerySelector(".span-vacuna")).ToContent();
    }
}
