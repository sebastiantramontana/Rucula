import getBondParameters from "./modules/bond-parameters.js";
import getCryptoParameters from "./modules/crypto-parameters.js";
import getWUParameters from "./modules/wu-parameters.js";
import { showDownloadingPackage } from "./modules/showDownloadingPackage.js";

addEventListener("load", async () => {
    try {
        await Blazor.start({
            loadBootResource: function (type, name, defaultUri, integrity) {
                showDownloadingPackage(name);
                return null;
            }
        });

        await StartShowChoices();
    }
    catch (ex) {
        console.log(`Algo salió mal: ${ex}`);
    }
});

async function StartShowChoices() {

    const titulosPublicosParameters = getBondParameters();
    const wuParameters = getWUParameters();
    const cryptoParameters = getCryptoParameters();

    const service = await getRuculaService();
    await service.StartShowChoices(titulosPublicosParameters, wuParameters, cryptoParameters);
}

async function getRuculaService() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const exports = await getAssemblyExports("Rucula.WebAssembly.dll");
    return exports.Rucula.WebAssembly.Program;
}