import showTitulosPublicos from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";


var dataIntervalId = null;

addEventListener("load", async () => {
    await Blazor.start();

    getAllData();
    startTimerGettingData();
    hookVisibilityEvent();
});


function hookVisibilityEvent() {
    document.addEventListener("visibilitychange", (e) => {

        if (document.visibilityState === "visible")
            startTimerGettingData();
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
    showTitulosPublicos();
}

