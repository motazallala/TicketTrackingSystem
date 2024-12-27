import { departmentTable } from './departmentTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { createDepartmentAsync } from '../../services/departmentServices.js';
$(document).ready(function () {
    $("#addDepartment").on("click", async function () {
        //add department modal that has a name and description field and a submit button
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = 'Add Department';
        const bodyContent = `
            <div class="form-group">
                <label for="name">Name:</label>
                <input type="text" class="form-control" id="name" name="name">
            </div>
                <div class="form-group">
                <label for="description">Description:</label>
                <input type="text" class="form-control" id="description" name="description">
           </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);
        $('#myModal').modal('show');
        $('#submit').on('click', async function () {
            //get the name and description values from the modal
            const addDepartmentDto = {
                name: $('#name').val().trim(),
                description: $('#description').val().trim(),
            };

            //call the createDepartmentAsync method from the departmentService
            const addResult = await createDepartmentAsync(JSON.stringify(addDepartmentDto))
            if (addResult.isSuccess) {
                //close the modal
                $('.modal').modal('hide');
                //reload the departmentTable
                departmentTable.ajax.reload(null, false);
            }
            else {
                showErrorModal(addResult.error.description);
            }
        });
    });
});
