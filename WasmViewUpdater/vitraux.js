"use strict";

globalThis.vitraux = {

    storedElements: {
        elements: {},
        getElementById(parent, id) {
            const element = parent.getElementById(id);
            return element;
        },

        getStoredElementById(parentObj, parentObjName, id, elementObjectName) {
            const element = this.elements[parentObjName][elementObjectName]
                ?? this.storeElementById(parentObj, parentObjName, id, elementObjectName);

            return [element];
        },

        storeElementById(parentObj, parentObjName, id, elementObjectName) {
            const element = this.getElementById(parentObj, id);
            this.elements[parentObjName][elementObjectName] = element;

            return element;
        },

        getElementsByQuerySelector(parent, querySelector) {
            return parent.querySelectorAll(querySelector);
        },

        getStoredElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
            const elements = this.elements[parentObjName][elementsObjectName]
                ?? this.storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName);

            return elements;
        },

        storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
            elements = this.getElementsByQuerySelector(parentObj, querySelector);
            this.elements[parentObjName][elementsObjectName] = elements;

            return elements;
        },

        getElementByTemplate(id) {
            return document.getElementById(id).content;
        },

        getStoredElementByTemplate(id, elementsObjectName) {
            const elements = this.elements["document"][elementsObjectName]
                ?? this.storeElementByTemplate(id, elementsObjectName);

            return elements;
        },

        storeElementByTemplate(id, elementsObjectName) {
            element = this.getElementsByTemplate(id);
            this.elements["document"][elementsObjectName] = element;

            return [element];
        }
    },
    updating: {
        vms: {},
        createUpdateFunction(vmName, code) {
            const func = new Function("vm", code);

            this.vms[vmName] = {
                function: func
            };
        },

        executeUpdateFunction(vmName, vm) {
            const func = this.vms[vmName].function;
            func(vm);
        },

        setElementsContent(elements, content) {
            for (const element in elements)
                element.textContent = content;
        },

        setElementsAttribute(elements, attribute, value) {
            for (const element in elements)
                element.setAttribute(attribute, value);
        }
    },

    executeCode(code) {
        const func = new Function(code);
        func();
    }
};
