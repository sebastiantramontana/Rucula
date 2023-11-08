using WasmViewUpdater.Model.Building.Selectors.Elements;
using WasmViewUpdater.Model.Building.Selectors.TableRows;

namespace WasmViewUpdater.Model.Building
{
    public class SACAR : IModelConfiguration<Persona>
    {
        private readonly IElementSelectorFactory _selectorFactory;
        private readonly IRowSelectionFactory _rowSelectionFactory;

        public SACAR(IElementSelectorFactory selectorFactory, IRowSelectionFactory rowSelectionFactory)
        {
            _selectorFactory = selectorFactory;
            _rowSelectionFactory = rowSelectionFactory;
        }

        public IEnumerable<IModel> Configure(IModelBuilder<Persona> modelBuilder)
        {
            var models = new List<IModel>
            {
                modelBuilder
                    .Value(a => a.Name)
                        .ToVoidElement(ById("algo-name"))
                        .ToAttribute("alt"),

                modelBuilder
                    .Value(a => a.Edad)
                        .ToContainerElement(FromTemplate("otro-template-id").AddTo(ById( "parent-to-add-id")).ById("child-target-id"))
                            .ToContent()
                        .ToVoidElement(ByQuerySelector(".p-otro > img"))
                            .ToAttribute("data-otro"),

                modelBuilder
                    .Collection(a => a.Mascotas)
                    .ToTable(ById("mascotas-id"))
                    .FillRows(_rowSelectionFactory.FromTemplate("row-template-id"))
                        .Value(m=>m.Name)
                            .ToContainerElement(ById( "cell-mascota-nombre-id")).ToContent()
                            .ToVoidElement(ById("anchor--cell-mascota-nombre-id")).ToAttribute("href")
                            .ToContainerElement(ById("another-anchor--cell-mascota-nombre-id")).ToAttribute("href")
                        .Value(m=>m.IsDespulgado)
                            .ToVoidElement(ById("some-despulgado-id")).ToAttribute("data-despulgado")
                        .Collection(m=>m.Vacunas)
                        .ToTable(ById("inner-table-vacunas"))
                        .FillRows(_rowSelectionFactory.FromTemplate("row-template-vacunas-id"))
                            .Value(v=>v.Name).ToContainerElement(ById("div-vacuna-id")).ToContent()
                            .Value(v=>v.DateApplied).ToContainerElement(ById("span-vacuna-id")).ToContent()
            };


            return models;
        }

        private IElementSelector ById(string id) => _selectorFactory.ById(id);
        private IFromTemplateSelector FromTemplate(string templateId) => _selectorFactory.FromTemplate(templateId);
        private IElementSelector ByQuerySelector(string querySelector) => _selectorFactory.ByQuerySelector(querySelector);

    }
}
