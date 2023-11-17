namespace WasmViewUpdater.Modeling.Building.Finalizables
{
    public interface IFinalizableTableRowModel<TEntity> : IModel, IBuildingValueModel<IFinalizableTableRowModel<TEntity>>, IFinalizableModelBuilder<TEntity, IFinalizableTableRowModel<TEntity>>
    {
    }
}
