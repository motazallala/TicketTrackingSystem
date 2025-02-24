import { roleTable } from './roleTable.js';
import { setupFormModal } from '../../utility/dataModalUtility.js';
import { updateRoleAsync } from '../../services/roleServices.js';
import { showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
import { FormManager } from '../../utility/formUitilty.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';


$(document).ready(function () {
    roleTable.on('click', '.dt-edit', async function () {
        let data = roleTable.row($(this).parents('tr')).data();

        const title = 'Edit Role';
        setupFormModal('edit-form', title, "Edit");

        const form = new FormManager("#edit-form", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner('myModalContent', false, false, true);
                const updateResult = await updateRoleAsync(data);
                hideSpinner('myModalContent');
                if (updateResult.isSuccess) {
                    showSuccessAlert(updateResult.successMessage);
                    $('.modal').modal('hide');
                    roleTable.ajax.reload(null, false);
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

        $('#myModal').modal('show');

    });
});