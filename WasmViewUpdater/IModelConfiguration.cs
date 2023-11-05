namespace WasmViewUpdater
{
    public interface IModel
    {

    }

    public interface IModelBuilder<T>
    {
        IModel ToElement();
        IModel ToTable();
        IModel Property<TReturn>(Func<T, TReturn> property);
    }

    public interface IModelConfiguration<T>
    {
        IEnumerable<IModel> Models { get; }
    }

    public abstract class ModelConfigurationBase<T> : IModelConfiguration<T>
    {
        private readonly IList<IModel> _models;

        protected ModelConfigurationBase()
        {
            _models = new List<IModel>();
        }

        public IEnumerable<IModel> Models => _models;

        protected void Add(IModel model)
        {
            _models.Add(model);
        }

        protected abstract void Configure(IModelBuilder<T> modelBuilder);
    }
}
