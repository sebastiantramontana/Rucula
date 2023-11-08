namespace WasmViewUpdater.Model.Building
{
    public interface IFinalizableValueModel : IModel, IValueModel<IFinalizableValueModel>
    {
    }

    public interface IFinalizableTableRowModel<TEntity> : IModel, IValueModel<IFinalizableTableRowModel<TEntity>>, IModelBuilder<TEntity, IFinalizableTableRowModel<TEntity>>
    {
    }
}
