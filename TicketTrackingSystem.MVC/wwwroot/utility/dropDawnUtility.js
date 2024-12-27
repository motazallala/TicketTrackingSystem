//make dropdown utility as export default
export function setupDropDownData(dropDownId, data) {
    // Clear existing dropdown content
    $(`#${dropDownId}`).empty();
    // Add new dropdown content
    data.forEach(item => {
        $(`#${dropDownId}`).append(`<option value="${item.id}">${item.name}</option>`);
    });
    // Enable dropdown
    $(`#${dropDownId}`).removeAttr('disabled');
    // Select the first item by default
    $(`#${dropDownId}`).val(data[0].id);
}

export default {
    setupDropDownData
}