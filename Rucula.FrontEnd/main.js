import showTitulosPublicos from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showDolarCrypto from "./modules/dolar-crypto.js";
import showDolarWesternUnion from "./modules/dolar-western-union.js";
import showBestChoice from "./modules/best-choice.js";

var dataIntervalId = null;
const numberFormater = new Intl.NumberFormat('es-AR', {
    minimumFractionDigits: 3,
    maximumFractionDigits: 3,
});

addEventListener("load", async () => {
    await Blazor.start();

    runGettingData();
    hookVisibilityEventToRun();
});

function runGettingData() {
    getAllData();
    startTimerGettingData();
}

function hookVisibilityEventToRun() {
    document.addEventListener("visibilitychange", (e) => {

        if (document.visibilityState === "visible")
            runGettingData();
        else
            stopTimerGettingData();
    });
}

function startTimerGettingData() {
    stopTimerGettingData();

    const OneMinute = 60000;
    dataIntervalId = setInterval(getAllData, OneMinute);
}

function stopTimerGettingData() {
    if (dataIntervalId)
        clearInterval(dataIntervalId);
}

async function getAllData() {
    showLoadingIndicator();

    const choices = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetChoices');

    showBestChoice(choices.winningChoice, numberFormater);
    showDolarBlue(choices.blue);
    showDolarCrypto(choices.dolarCrypto);
    showDolarWesternUnion(choices.dolarWesternUnion);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);

    hideLoadingIndicator();
}

function showLoadingIndicator() {
    let indicator = document.getElementById("loading-indicator");
    indicator.style.display = "inline";
}

function hideLoadingIndicator() {
    let indicator = document.getElementById("loading-indicator");
    indicator.style.display = "none";
}

