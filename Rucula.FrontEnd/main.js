import { showTitulosPublicos, getCommissions as getBondCommissions, disableCommissions as disableBondCommissions, enableCommissions as enableBondCommissions } from "./modules/titulos-publicos.js";
import showDolarBlue from "./modules/dolar-blue.js";
import showDolarCrypto from "./modules/dolar-crypto.js";
import { showDolarWesternUnion, getParameters as getWuParameters, disableParameters as disableWuParameters, enableParameters as enableWuParameters } from "./modules/dolar-western-union.js";
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

    disableBondCommissions();
    disableWuParameters();
    showLoadingIndicator();

    const choices = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetChoices', getBondCommissions(), getWuParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showDolarBlue(getValueFromOptional(choices.blue));
    showDolarCrypto(getValueFromOptional(choices.dolarCrypto));
    showDolarWesternUnion(getValueFromOptional(choices.dolarWesternUnion));
    showDolarDiarco(getValueFromOptional(choices.dolarDiarco));
    showTitulosPublicos(choices.rankingTitulos, numberFormater);

    showBestChoiceElement();
    enableBondCommissions();
    enableWuParameters();

    isGettingData = false;
}

function recalculateChoices() {
    if (isGettingData)
        return;

    isGettingData = true;

    disableBondCommissions();
    disableWuParameters();
    showLoadingIndicator();

    const choices = DotNet.invokeMethod('Rucula.WebAssembly', 'RecalculateChoices', getBondCommissions(), getWuParameters());

    showBestChoice(choices.winningChoice, numberFormater);
    showTitulosPublicos(choices.rankingTitulos, numberFormater);
    showDolarWesternUnion(getValueFromOptional(choices.dolarWesternUnion));

    showBestChoiceElement();
    enableBondCommissions();
    enableWuParameters();

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
