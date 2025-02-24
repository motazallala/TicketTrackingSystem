import { departmentTable } from './departmentTable.js';
import { setupFormModal } from '../../utility/dataModalUtility.js';
import { showSuccessAlert, showErrorAlert } from '../../utility/alertUtility.js';
import { FormManager } from '../../utility/formUitilty.js';
import {  showSpinner, hideSpinner } from '../../utility/spinnerUtility.js';
import { updateDepartmentAsync } from '../../services/departmentServices.js';

$(document).ready(function () {
    departmentTable.on('click', '.dt-edit', async function () {
        let data = departmentTable.row($(this).parents('tr')).data();


        const title = 'Edit Department';
        setupFormModal('edit-form', title, "Edit");


        const form = new FormManager("#edit-form", {
            showAlerts: false,
            resetOnSubmit: false,
            onSubmit: async (data) => {
                showSpinner('myModalContent', false, false, true);
                const updateResult = await updateDepartmentAsync(data);
                hideSpinner('myModalContent');
                if (updateResult.isSuccess) {
                    showSuccessAlert(updateResult.successMessage);
                    $('.modal').modal('hide');
                    departmentTable.ajax.reload(null, false);
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
            hidden : true
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