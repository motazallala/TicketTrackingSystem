import { _hashModelId , _ModalContentId } from '../../utility/globals.js';
import { roleTable } from './roleTable.js';
import { setupFormModal } from '../../utility/dataModalUtility.js';
import { createRoleAsync } from '../../services/roleServices.js';
import { FormManager } from '../../utility/formUitilty.js';
import { showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';
$(document).ready(function () {
    $("#addButton").on('click', async function () {
        //add department modal that has a name and description field and a submit button
        const title = 'Add Role';
        setupFormModal('myForm', title, 'Add');



        const form = new FormManager("#myForm", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner(_ModalContentId, false, false, true);
                // Call the createDepartmentAsync method using the DTO object
                const addResult = await createRoleAsync(data)
                hideSpinner(_ModalContentId);
                if (addResult.isSuccess) {
                    showSuccessAlert(addResult.successMessage);
                    // Hide the modal on success
                    $('#myModal').modal('hide');
                    // Reload the department table without resetting pagination
                    roleTable.ajax.reload(null, false);
                } else if (addResult.error.code === 409) {
                    showErrorAlert(addResult.error.description);
                    // Show error modal if there is a problem
                    form.handleServerErrors(addResult.error.validation);
                }
                else {
                    showErrorAlert(addResult.error.description);
                }
            }
        });
        form.addField({
            name: "name",
            label: "Name",
            type: "text",
            required: false,
            placeholder: "Enter Name",
        });
        $(_hashModelId).modal('show');

    });
});
