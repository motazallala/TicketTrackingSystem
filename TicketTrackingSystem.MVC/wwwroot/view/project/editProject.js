import { projectTable } from './projectTable.js';
import { setupModalData, showErrorModal, setupFormModal } from '../../utility/dataModalUtility.js';
import { updateProjectAsync } from '../../services/projectServices.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';
import { FormManager } from '../../utility/formUitilty.js';
import { showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
$(document).ready(function () {
    projectTable.on('click', '.dt-edit', async function () {
        let data = projectTable.row($(this).parents('tr')).data();

        const title = 'Edit Department';
        setupFormModal('edit-form', title, "Edit");

        const form = new FormManager("#edit-form", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner('myModalContent', false, false, true);
                const updateResult = await updateProjectAsync(data);
                hideSpinner('myModalContent');
                if (updateResult.isSuccess) {
                    showSuccessAlert(updateResult.successMessage);
                    $('.modal').modal('hide');
                    projectTable.ajax.reload(null, false);
                } else if (updateResult.error.code === 409) {
                    showErrorAlert(updateResult.error.description);
                    // Show error modal if there is a problem
                    form.handleServerErrors(updateResult.error.validation);
                }
                else {
                    showErrorAlert(updateResult.error.description);
                }
            }
        });
        form.addField({
            name: "id",
            label: "Id",
            type: "hidden",
            value: data.id,
            hidden: true
        });
        form.addField({
            name: "name",
            label: "Name",
            type: "text",
            required: false,
            value: data.name,
            placeholder: "Enter Name",
        });

        form.addField({
            name: "description",
            label: "Description",
            type: "text",
            required: false,
            value: data.description,
            placeholder: "Enter Description",
        });

        $('#myModal').modal('show');

    });
});