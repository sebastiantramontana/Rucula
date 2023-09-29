import showTitulosPublicos from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";

addEventListener("load", async () => {
    await Blazor.start();

    showDolarBlue();
    showTitulosPublicos();
});


