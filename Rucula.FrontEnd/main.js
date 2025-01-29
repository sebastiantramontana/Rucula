import showTitulosPublicos from "./modules/titulos-publicos.js";
import titulosPublicosParameters from "./modules/titulos-publicos-commissions.js";
import showDolarCrypto from "./modules/dolar-crypto.js";
import cryptoParameters from "./modules/dolar-crypto-parameters.js";
import showDolarWesternUnion from "./modules/dolar-western-union.js";
import wuParameters from "./modules/dolar-western-union-parameters.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showBestChoice from "./modules/best-choice.js";
import { notify } from "./modules/notify.js";

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
    applyButton.addEventListener("click", runRecalculateChoices);
}

function addApplyWuParametersClickListener() {
    const applyButton = document.getElementById("apply-amount-to-send-wu");
    applyButton.addEventListener("click", runRecalculateChoices);
}

function addApplyCryptoParametersClickListener() {
    const applyButton = document.getElementById("apply-trading-volume-crypto");
    applyButton.addEventListener("click", runRecalculateChoices);
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

    const choices = await getChoices(titulosPublicosParameters.getParameters(), wuParameters.getParameters(), cryptoParameters.getParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showDolarBlue(choices.blue);
    showDolarCrypto(choices.rankingCryptos, numberFormater);
    showDolarWesternUnion(choices.dolarWesternUnion, numberFormater);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);

    showBestChoiceElement();
    titulosPublicosParameters.enableParameters();
    wuParameters.enableParameters();
    cryptoParameters.enableParameters();

    isGettingData = false;
}

async function runRecalculateChoices() {
    if (isGettingData)
        return;

    isGettingData = true;

    titulosPublicosParameters.disableParameters();
    wuParameters.disableParameters();
    cryptoParameters.disableParameters();

    showLoadingIndicator();

    const choices = await recalculateChoices(titulosPublicosParameters.getParameters(), wuParameters.getParameters(), cryptoParameters.getParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);
    showDolarWesternUnion(choices.dolarWesternUnion, numberFormater);

    showBestChoiceElement();
    titulosPublicosParameters.enableParameters();
    wuParameters.enableParameters();
    cryptoParameters.enableParameters();

    isGettingData = false;
}

async function getChoices(titulosPublicosParameters, wuParameters, cryptoParameters) {
    const service = await getRuculaService();
    const choicesJson = await service.GetChoices(titulosPublicosParameters, wuParameters, cryptoParameters);

    return JSON.parse(choicesJson);
}

async function recalculateChoices(titulosPublicosParameters, wuParameters, cryptoParameters) {
    const service = await getRuculaService();
    const choicesJson = await service.RecalculateChoices(titulosPublicosParameters, wuParameters, cryptoParameters);

    return JSON.parse(choicesJson);
}

async function getRuculaService() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const exports = await getAssemblyExports("Rucula.WebAssembly.dll");
    return exports.Rucula.WebAssembly.Program;
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