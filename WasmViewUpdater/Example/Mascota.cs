using WasmViewUpdater.Example;

namespace WasmViewUpdater.Model.Building
{
    public record Mascota(string Name, bool IsDespulgado, IEnumerable<Vacuna> Vacunas);
}
