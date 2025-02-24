import { projectTable } from './projectTable.js';
import { setupFormModal } from '../../utility/dataModalUtility.js';
import { createProjectAsync } from '../../services/projectServices.js';
import { showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';
import { FormManager } from '../../utility/formUitilty.js';

$(document).ready(function () {
    $("#addButton").on('click', async function () {
        const title = 'Add Project';
        setupFormModal('myForm', title, 'Add');

        const form = new FormManager("#myForm", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner('myModalContent', false, false, true);
                // Call the createDepartmentAsync method using the DTO object
                const addResult = await createProjectAsync(data);
                hideSpinner('myModalContent');
                if (addResult.isSuccess) {
                    showSuccessAlert(addResult.successMessage);
                    // Hide the modal on success
                    $('#myModal').modal('hide');
                    // Reload the department table without resetting pagination
                    projectTable    .ajax.reload(null, false);
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
        form.addField({
            name: "description",
            label: "Description",
            type: "text",
            required: false,
            placeholder: "Enter Description",
        });


        $('#myModal').modal('show');
    });

});
