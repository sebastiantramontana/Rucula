import showTitulosPublicos from "./modules/titulos-publicos.js";
import titulosPublicosParameters from "./modules/titulos-publicos-commissions.js";
import showDolarCrypto from "./modules/dolar-crypto.js";
import cryptoParameters from "./modules/dolar-crypto-parameters.js";
import showDolarWesternUnion from "./modules/dolar-western-union.js";
import wuParameters from "./modules/dolar-western-union-parameters.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showDolarDiarco from "./modules/dolar-diarco.js";
import showBestChoice from "./modules/best-choice.js";
import getValueFromOptional from "./optional-value.js";

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

        addApplyBondCommissionsClickListener();
        addApplyWuParametersClickListener();
        addApplyCryptoParametersClickListener();

        await runGettingData();
        hookVisibilityEventToRun();
    }
    catch (ex) {
        console.log(`Algo salió mal: ${ex}`);
    }
});

function addApplyBondCommissionsClickListener() {
    const applyButton = document.getElementById("apply-bond-commissions-button");
    applyButton.addEventListener("click", recalculateChoices);
}

function addApplyWuParametersClickListener() {
    const applyButton = document.getElementById("apply-amount-to-send-wu");
    applyButton.addEventListener("click", recalculateChoices);
}

function addApplyCryptoParametersClickListener() {
    const applyButton = document.getElementById("apply-trading-volume-crypto");
    applyButton.addEventListener("click", recalculateChoices);
}

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

    titulosPublicosParameters.disableParameters();
    wuParameters.disableParameters();
    cryptoParameters.disableParameters();

    showLoadingIndicator();

    const choices = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetChoices', titulosPublicosParameters.getParameters(), wuParameters.getParameters(), cryptoParameters.getParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showDolarBlue(getValueFromOptional(choices.blue));
    showDolarCrypto(choices.rankingCryptos, numberFormater);
    showDolarWesternUnion(getValueFromOptional(choices.dolarWesternUnion), numberFormater);
    showDolarDiarco(getValueFromOptional(choices.dolarDiarco));
    showTitulosPublicos(choices.rankingTitulos, numberFormater);

    showBestChoiceElement();
    titulosPublicosParameters.enableParameters();
    wuParameters.enableParameters();
    cryptoParameters.enableParameters();

    isGettingData = false;
}

async function recalculateChoices() {
    if (isGettingData)
        return;

    isGettingData = true;

    titulosPublicosParameters.disableParameters();
    wuParameters.disableParameters();
    cryptoParameters.disableParameters();

    showLoadingIndicator();

    const choices = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'RecalculateChoices', titulosPublicosParameters.getParameters(), wuParameters.getParameters(), cryptoParameters.getParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);
    showDolarWesternUnion(getValueFromOptional(choices.dolarWesternUnion), numberFormater);

    showBestChoiceElement();
    titulosPublicosParameters.enableParameters();
    wuParameters.enableParameters();
    cryptoParameters.enableParameters();

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
