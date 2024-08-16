﻿import showTitulosPublicos from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showDolarCrypto from "./modules/dolar-crypto.js";
import showDolarWesternUnion from "./modules/dolar-western-union.js";
import showBestChoice from "./modules/best-choice.js";

let dataIntervalId = null;
const numberFormater = new Intl.NumberFormat('es-AR', {
    minimumFractionDigits: 3,
    maximumFractionDigits: 3,
});

addEventListener("load", async () => {
    try {
        await Blazor.start({
            loadBootResource: function (type, name, defaultUri, integrity) {
                notify(name);
                return null;
            }
        });

        await runGettingData();
        hookVisibilityEventToRun();
    }
    catch (ex) {
        console.log(`Algo salió mal: ${ex}`);
    }
});

async function runGettingData() {
    await getAllData();
    startTimerGettingData();
}

function hookVisibilityEventToRun() {
    document.addEventListener("visibilitychange", async (e) => {

        if (document.visibilityState === "visible")
            await runGettingData();
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

let isGettingData = false;

async function getAllData() {
    if (isGettingData)
        return;

    isGettingData = true;

    showLoadingIndicator();

    const choices = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetChoices');

    showBestChoice(choices.winningChoice, numberFormater);
    showDolarBlue(choices.blue);
    showDolarCrypto(choices.dolarCrypto);
    showDolarWesternUnion(choices.dolarWesternUnion);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);

    showBestChoiceElement();

    isGettingData = false;
}

function showLoadingIndicator() {
    hideBestChoice();
    hideDownloadingIndicator();
    showLoadingIndicatorElement();
    showAllIndicatorsContainer();
}

function showBestChoiceElement() {

    showElementAsInlineBlock("mejor-opcion");
    hideAllIndicatorsContainer();
}

function hideDownloadingIndicator() {
    hideElement("downloading-packages-indicator");
}

function showAllIndicatorsContainer() {
    showElementAsInlineBlock("all-indicators-container");
}

function hideAllIndicatorsContainer() {
    hideElement("all-indicators-container");
}

function hideBestChoice() {
    hideElement("mejor-opcion");
}

function showLoadingIndicatorElement() {
    showElementAsInlineBlock("loading-indicator");
}

function showElementAsInlineBlock(id) {
    const element = document.getElementById(id);
    element.style.display = "inline-block";
}

function hideElement(id) {
    const element = document.getElementById(id);
    element.style.display = "none";
}

export function notify(message) {
    const msgElement = document.getElementById("notify-progress");
    msgElement.textContent = message;
}
