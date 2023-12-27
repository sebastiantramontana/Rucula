using WasmViewUpdater.Modeling;
using WasmViewUpdater.Modeling.Building;

namespace WasmViewUpdater.Test.Example;

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
                            .AppendTo(ById("parent-to-add-id"))
                            .ToChild(ById("child-target-id")))
                    .ToContent()
                .ToElement(ByQuerySelector(".p-otro > img"))
                    .ToAttribute("data-otro")
            .Collection(a => a.Mascotas)
                .ToTable(ById("mascotas-table-id"))
                .RowsFrom(Template("row-template-id"))
                    .Value(m => m.Name)
                        .ToContainerElement(ById("cell-mascota-nombre-id")).ToContent()
                        .ToElement(ById("anchor-cell-mascota-nombre-id")).ToAttribute("href")
                        .ToContainerElement(ById("another-anchor-cell-mascota-nombre-id")).ToAttribute("href")
                    .Value(m => m.IsDespulgado)
                        .ToElement(ById("some-despulgado-id")).ToAttribute("data-despulgado")
                    .Collection(m => m.Vacunas)
                        .ToTable(ById("inner-table-vacunas"))
                        .RowsFrom(Template("row-template-vacunas-id"))
                            .Value(v => v.Name).ToContainerElement(ById("div-vacuna-id")).ToContent()
                            .Value(v => v.DateApplied).ToContainerElement(ById("span-vacuna-id")).ToContent();
    }
}
