export default function getValueFromOptional(optionalObj) {
    const newValue = Object.create(null);

    for (const property in optionalObj.value) {
        newValue[property] = optionalObj.hasValue ? optionalObj.value[property] : "-";
    }

    return newValue;
}
