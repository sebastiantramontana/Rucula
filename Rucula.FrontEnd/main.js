import fillTitulosTable from "./modules/titulos-table.js";

addEventListener("load", async () => {
    await Blazor.start();

    await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    const tabla = document.getElementById("tabla-titulos");

    fillTitulosTable(tabla, titulos);
});
