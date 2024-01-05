"use strict";

const vitraux = {
    vms: {},
    storedElements: {},

    getElementById(parent, id, elementObjectName) {
        const element = this.storedElements[parent][elementObjectName]
            ?? this.storeElementById(parent, id, elementObjectName);

        return [element];
    },

    storeElementById(parent, id, elementObjectName) {
        const element = parent.getElementById(id);
        this.storedElements[parent][elementObjectName] = element;

        return element;
    },

    getElementsByQuerySelector(parent, querySelector, elementsObjectName) {
        const elements = this.storedElements[parent][elementsObjectName]
            ?? storeElementsByQuerySelector(parent, querySelector, elementsObjectName);

        return elements;
    },

    storeElementsByQuerySelector(parent, querySelector, elementsObjectName) {
        elements = parent.querySelectorAll(querySelector);
        this.storedElements[parent][elementsObjectName] = elements;

        return elements;
    },

    setElementsContent(elements, content) {
        for (const element in elements)
            element.textContent = content;
    },

    setElementsAttribute(elements, attribute, value) {
        for (const element in elements)
            element.setAttribute(attribute, value);
    }
};

//global functions for update
const vitrauxCreateUpdateFunction = vitrauxCreateUpdateFunction ?? ((vmName, code) => {
    const func = new Function("vm", code);

    vitraux.vms[vmName] = {
        function: func
    };
});

const vitrauxExecuteUpdateFunction = vitrauxExecuteUpdateFunction ?? ((vmName, vm) => {
    const func = vitraux.vms[vmName].function;
    func(vm);
});
