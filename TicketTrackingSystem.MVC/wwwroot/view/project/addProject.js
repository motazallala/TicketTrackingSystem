import { projectTable } from './projectTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { createProjectAsync } from '../../services/projectServices.js';
$(document).ready(function () {
    $("#addButton").on('click', async function () {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = 'Add Project';
        const bodyContent = `
            <div class="form-group my-1">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name" name="name">
            </div>
            <div class="form-group my-1">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="description" name="description">
            </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);
        $('#myModal').modal('show');
        $('#submit').off('click').on('click', async function () {

            try {
                const dto = gatherFormData();
                //call the createProjectAsync method from the projectServices
                const addResult = await createProjectAsync(dto)
                if (addResult.isSuccess) {
                    //close the modal
                    $('.modal').modal('hide');
                    //reload the projectTable
                    projectTable.ajax.reload(null, false);

                }
                else {
                    showErrorModal(addResult.error.description);
                }

            } catch (e) {
                showErrorModal('An error occurred while loading the data.');
            }
        });
    });

    function gatherFormData() {
        return {
            name: $('#name').val(),
            description: $('#description').val(),
        };
    }
});
