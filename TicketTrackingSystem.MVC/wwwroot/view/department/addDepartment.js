import { departmentTable } from './departmentTable.js';
import { setupFormModal } from '../../utility/dataModalUtility.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';
import { showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
import { FormManager } from '../../utility/formUitilty.js';
import { createDepartmentAsync } from '../../services/departmentServices.js';

$(document).ready(function () {
    $("#addDepartment").on("click", async function () {
        const title = 'Add Department';
        setupFormModal('myForm', title, 'Add');

        const form = new FormManager("#myForm", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner('myModalContent', false, false, true);
                // Call the createDepartmentAsync method using the DTO object
                const addResult = await createDepartmentAsync(data);
                hideSpinner('myModalContent');
                if (addResult.isSuccess) {
                    showSuccessAlert(addResult.successMessage);
                    // Hide the modal on success
                    $('#myModal').modal('hide');
                    // Reload the department table without resetting pagination
                    departmentTable.ajax.reload(null, false);
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
            //validation: ['required']
        });

        form.addField({
            name: "description",
            label: "Description",
            type: "text",
            required: false,
            placeholder: "Enter Description",
            //validation: ['required', 'min:2','date']
            //validation: ['required']
        });

        // Show the modal
        $('#myModal').modal('show');

    });


});
