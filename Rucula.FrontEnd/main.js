import fillTitulosTable from "./modules/titulos-table.js";

addEventListener("load", async () => {
    await Blazor.start();

    try {
        const titulos = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    }
    catch (error) {
        alert(`error: ${error}`);
    }

    const tabla = document.getElementById("tabla-titulos");

    fillTitulosTable(tabla, titulos);
});
