using WasmViewUpdater.Model.Selectors;

namespace WasmViewUpdater.Model.Building
{
    public interface IModelBuilder<TEntity, TFinalizable>
        where TFinalizable : IModel
    {
        IValueModel<TFinalizable> Value<TReturn>(Func<TEntity, TReturn> func);
        ICollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func);
    }

    public interface IModelBuilder<TEntity> : IModelBuilder<TEntity, IFinalizableValueModel>
    {
    }

    public interface ICollectionModel<TEntity>
    {
        IToTableModel<TEntity> ToTable(ISelector selector);
    }

    public interface IToTableModel<TEntity>
    {
        IFinalizableTableRowModel<TEntity> FillRows(IRowSelection rowSelection);
    }

    public interface IRowSelection
    {
        internal RowSelectionFrom From { get; }
        internal string? Value { get; }
    }

    internal enum RowSelectionFrom
    {
        FromTemplate,
        CopyFirst,
        CopyLast
    }

    public interface IRowSelectionFactory
    {
        IRowSelection FromTemplate(string templateId);
        IRowSelection CopyFirstRow();
        IRowSelection CopyLasttRow();
    }

    /// <summary>
    /// SACAR
    /// </summary>}

    public class Vacuna
    {
        public string Name { get; set; }
        public DateTime DateApplied { get; set; }
    }

    public class Mascota
    {
        public string Name { get; set; }
        public bool IsDespulgado { get; set; }

        public IEnumerable<Vacuna> Vacunas { get; set; }
    }

    public class Persona
    {
        public string Name { get; set; }
        public byte Edad { get; set; }
        public IEnumerable<Mascota> Mascotas { get; set; }
    }

    public class SACAR : IModelConfiguration<Persona>
    {
        private readonly ISelectorFactory _selectorFactory;
        private readonly IRowSelectionFactory _rowSelectionFactory;

        public SACAR(ISelectorFactory selectorFactory, IRowSelectionFactory rowSelectionFactory)
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

        private ISelector ById(string id) => _selectorFactory.ById(id);
        private IFromTemplateSelector FromTemplate(string templateId) => _selectorFactory.FromTemplate(templateId);
        private ISelector ByQuerySelector(string querySelector) => _selectorFactory.ByQuerySelector(querySelector);

    }
}
