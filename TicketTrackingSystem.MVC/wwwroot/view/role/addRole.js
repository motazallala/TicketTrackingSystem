import { roleTable } from './roleTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { createRoleAsync } from '../../services/roleServices.js';
$(document).ready(function () {
    $("#addButton").on('click', async function () {
        //add department modal that has a name and description field and a submit button
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = 'Add Role';
        const bodyContent = `
            <div class="form-group my-1">
                <label for="name">Name:</label>
                <input type="text" class="form-control" id="name" name="name">
            </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);
        $('#myModal').modal('show');
        $('#submit').off('click').on('click', async function () {

            //call the createDepartmentAsync method from the departmentService
            const addResult = await createRoleAsync($('#name').val().trim())
            if (addResult.isSuccess) {
                //close the modal
                $('.modal').modal('hide');
                //reload the departmentTable
                roleTable.ajax.reload(null, false);

            }
            else {
                showErrorModal(addResult.error.description);
            }
        });
    });
});
