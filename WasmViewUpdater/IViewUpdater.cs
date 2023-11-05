namespace WasmViewUpdater
{
    public interface IViewUpdater<in T>
    {
        void Update(T model);
    }
}