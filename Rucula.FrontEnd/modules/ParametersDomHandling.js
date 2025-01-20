export default class ParametersDomHandling {
    #settingsKey;
    #defaultParameters;
    #inputs;
    #applyButton;

    constructor(inputsCallback, applyButtonCallback, getParametersFromInputsCallback, settingsKey, defaultParameters, setParametersToInputsCallback) {
        this.#settingsKey = settingsKey;
        this.#defaultParameters = defaultParameters;

        this.#suscribeToDOMContentLoaded(applyButtonCallback, inputsCallback, getParametersFromInputsCallback, settingsKey, defaultParameters, setParametersToInputsCallback);
    }

    disableParameters() {
        this.#toggleEnablingParameters(true, this.#inputs, this.#applyButton);
    }

    enableParameters() {
        this.#toggleEnablingParameters(false, this.#inputs, this.#applyButton);
    }

    getParameters() {
        return this.#getSavedParametersOrDefault(this.#settingsKey, this.#defaultParameters);
    }

    #suscribeToDOMContentLoaded(applyButtonCallback, inputsCallback, getParametersFromInputsCallback, settingsKey, defaultParameters, setParametersToInputsCallback) {
        window.addEventListener("DOMContentLoaded", () => {
            this.#inputs = inputsCallback();
            this.#applyButton = applyButtonCallback();

            this.#setCurrentDisabledToApplyButton(this.#applyButton);
            this.#addAllParameterInputEventListeners(this.#inputs, this.#applyButton);
            this.#addApplyParametersClickListener(this.#applyButton, getParametersFromInputsCallback, settingsKey);
            this.#loadParametersInputs(settingsKey, defaultParameters, setParametersToInputsCallback);
        });
    }

    #setCurrentDisabledToApplyButton(applyButton) {
        applyButton.currentDisabled = applyButton.disabled;
    }

    #addAllParameterInputEventListeners(inputs, applyButton) {
        for (const input of inputs) {
            this.#addParameterInputEventListener(input, inputs, applyButton);
        }
    }

    #addParameterInputEventListener(inputToAddListener, inputs, applyButton) {
        inputToAddListener.addEventListener('input', () => {

            let isEnabled = true;

            for (const input of inputs) {
                isEnabled &&= input.checkValidity();
            }

            applyButton.disabled = !isEnabled;
            this.#setCurrentDisabledToApplyButton(applyButton);
        });
    }

    #loadParametersInputs(settingsKey, defaultParameters, setParametersToInputsCallback) {
        const parametersSettings = this.#getSavedParametersOrDefault(settingsKey, defaultParameters);
        setParametersToInputsCallback(parametersSettings);
    }

    #addApplyParametersClickListener(applyButton, getParametersFromInputsCallback, settingsKey) {
        applyButton.addEventListener("click", () => {

            const parameters = getParametersFromInputsCallback();
            localStorage.setItem(settingsKey, JSON.stringify(parameters));
            applyButton.disabled = true;
            this.#setCurrentDisabledToApplyButton(applyButton);
        });
    }

    #getSavedParametersOrDefault(settingsKey, defaultParameters) {
        const parametersSettingsJson = localStorage.getItem(settingsKey);

        return parametersSettingsJson !== null
            ? JSON.parse(parametersSettingsJson)
            : defaultParameters;
    }

    #toggleEnablingParameters(isDisabled, inputs, applyButton) {

        for (const input of inputs) {
            input.disabled = isDisabled
        }

        applyButton.disabled = (isDisabled) ? true : applyButton.currentDisabled || false;
    }
}