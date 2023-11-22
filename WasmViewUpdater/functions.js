"use strict";

//Functions for update

const viewUpdaterUpdates = {};

const viewUpdaterCreateUpdateFunction = viewUpdaterCreateUpdateFunction || ((funcName, code) => {
    const func = new Function("vm", code);
    viewUpdaterUpdates[funcName] = func;
});

const viewUpdaterExecuteUpdateFunction = viewUpdaterExecuteUpdateFunction || ((funcName, vm) => {
    const func = viewUpdaterUpdates[funcName];
    func(vm);
});

//Function for HTML Elements

const viewUpdaterGetElementById = viewUpdaterGetElementById || ((parent, id) => [parent.getElementById(id)]);

const viewUpdaterGetAllElementsByQuerySelector
    = viewUpdaterGetAllElementsByQuerySelector || ((parent, querySelector) => parent.querySelectorAll(querySelector));

const viewUpdaterSetElementsContent = viewUpdaterSetElementsContent || ((elements, content) => {
    for (const element in elements)
        element.innerHTML = content;
});

const viewUpdaterSetElementsAttribute = viewUpdaterSetElementsAttribute || ((elements, attribute, value) => {
    for (const element in elements)
        element.setAttribute(attribute, value);
});