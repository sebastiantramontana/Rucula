"use strict";

globalThis.vitraux = {

    storedElements: {
        elements: {},
        getElementByIdAsArray(parent, id) {
            const element = parent.getElementById
                ? parent.getElementById(id)
                : parent.querySelector("#".concat(id));

            return [element];
        },

        getStoredElementByIdAsArray(parentObj, parentObjName, id, elementObjectName) {
            const elementArray = this.elements[parentObjName][elementObjectName]
                ?? this.storeElementByIdAsArray(parentObj, parentObjName, id, elementObjectName);

            return elementArray;
        },

        storeElementByIdAsArray(parentObj, parentObjName, id, elementObjectName) {
            const elementArray = this.getElementByIdAsArray(parentObj, id);
            this.elements[parentObjName][elementObjectName] = elementArray;

            return elementArray;
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

        getElementByTemplateAsArray(id) {
            return [document.getElementById(id).content];
        },

        getStoredElementByTemplateAsArray(id, elementsObjectName) {
            const elements = this.elements["document"][elementsObjectName]
                ?? this.storeElementByTemplateAsArray(id, elementsObjectName);

            return elements;
        },

        storeElementByTemplateAsArray(id, elementsObjectName) {
            element = this.getElementByTemplateAsArray(id);
            this.elements["document"][elementsObjectName] = element;

            return element;
        },
        QueryTemplateChildNoChild(templateContent) {
            return templateContent;
        },

        QueryTemplateChildById(templateContent, id) {
            return globalThis.vitraux.storedElements.getElementByIdAsArray(templateContent, id);
        },

        QueryTemplateChildByQuerySelector(templateContent, querySelector) {
            return globalThis.vitraux.storedElements.getElementsByQuerySelector(templateContent, querySelector);
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
        },

        UpdateByTemplate(templateContent, addToElements, toChildQueryFunction, updateTemplateChildFunction) {

            for (const addToElement of addToElements) {
                const clonedTemplateContent = templateContent.cloneNode(true);
                targetTemplateChildElements = toChildQueryFunction(clonedTemplateContent);
                updateTemplateChildFunction(targetTemplateChildElements);
                addToElement.appendChild(clonedTemplateContent); //Falta el shadow DOM
            }
        }
    },

    executeCode(code) {
        const func = new Function(code);
        func();
    }
};
