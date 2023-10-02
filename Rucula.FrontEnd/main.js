import showTitulosPublicos from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showDolarCrypto from "./modules/dolar-crypto.js";


var dataIntervalId = null;

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

function getAllData() {
    showDolarBlue();
    showDolarCrypto();
    showTitulosPublicos();
}

