"use strict";

const vitraux = {
    vms: {},
    storedElements: {},

    ExecuteCode(code) {
        const func = new Function(code);
        func();
    },

    getElementById(parent, id) {
        const element = parent.getElementById(id);
        return element;
    },

    getStoredElementById(parentObj, parentObjName, id, elementObjectName) {
        const element = this.storedElements[parentObjName][elementObjectName]
            ?? this.storeElementById(parentObj, parentObjName, id, elementObjectName);

        return [element];
    },

    storeElementById(parentObj, parentObjName, id, elementObjectName) {
        const element = this.getElementById(parentObj, id);
        this.storedElements[parentObjName][elementObjectName] = element;

        return element;
    },

    getElementsByQuerySelector(parent, querySelector) {
        return parent.querySelectorAll(querySelector);
    },

    getStoredElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
        const elements = this.storedElements[parentObjName][elementsObjectName]
            ?? this.storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName);

        return elements;
    },

    storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
        elements = this.getElementsByQuerySelector(parentObj, querySelector);
        this.storedElements[parentObjName][elementsObjectName] = elements;

        return elements;
    },

    getElementByTemplate(id) {
        return document.getElementById(id).content;
    },

    getStoredElementByTemplate(id, elementsObjectName) {
        const elements = this.storedElements["document"][elementsObjectName]
            ?? this.storeElementByTemplate(id, elementsObjectName);

        return elements;
    },

    storeElementByTemplate(id, elementsObjectName) {
        element = this.getElementsByTemplate(id);
        this.storedElements["document"][elementsObjectName] = element;

        return [element];
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
