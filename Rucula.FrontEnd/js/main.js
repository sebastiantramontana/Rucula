import getBondParameters from "./bond-parameters.js";
import getCryptoParameters from "./crypto-parameters.js";
import getWUParameters from "./wu-parameters.js";
import getDolarAppParameters from "./dolarapp-parameters.js";

import { notifyMessage, removeDownloadingPackagesIndicator } from "./notification.js";

addEventListener("load", async () => {
    try {
        await Blazor.start({
            loadBootResource: function (type, name, defaultUri, integrity) {
                showDownloadingPackageName(name);
                return null;
            }
        });

        showAwaitingMessage();
        removeDownloadingPackagesIndicator();

        await StartShowOptions();
    }
    catch (ex) {
        console.log(`Algo salió mal: ${ex}`);
    }
});

function showDownloadingPackageName(packageName) {
    notifyMessage(`Cargando ${packageName}...`);
}

function showAwaitingMessage() {
    notifyMessage(`Preparando todo...`);
}

async function StartShowOptions() {

    const titulosPublicosParameters = getBondParameters();
    const wuParameters = getWUParameters();
    const cryptoParameters = getCryptoParameters();
    const dolaAppParameters = getDolarAppParameters();

    const service = await getRuculaService();
    await service.StartShowOptions(titulosPublicosParameters, wuParameters, cryptoParameters, dolaAppParameters);
}

async function getRuculaService() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const exports = await getAssemblyExports("Rucula.WebAssembly.dll");
    return exports.Rucula.WebAssembly.Program;
}