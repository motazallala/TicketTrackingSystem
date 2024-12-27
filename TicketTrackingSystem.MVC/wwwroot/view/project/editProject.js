import { projectTable } from './projectTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { updateProjectAsync } from '../../services/projectServices.js';

$(document).ready(function () {
    projectTable.on('click', '.dt-edit', async function () {
        let data = projectTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Edit Department';
        const bodyContent = `
            <input type="hidden" class="form-control" id="id" name="id" value="${data.id}" >
            <div class="form-group my-1">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name" name="name">
            </div>
            <div class="form-group my-1">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="description" name="description">
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const editDepartment = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, editDepartment]);
        populateProjectForm(data);
        $('#myModal').modal('show');
        $('#submit').on('click', async function () {
            const editProjectDto = gatherFormData();

            try {
                const updateResult = await updateProjectAsync(JSON.stringify(editProjectDto));

                if (updateResult.isSuccess) {

                    $('.modal').modal('hide');
                    projectTable.ajax.reload(null, false);
                }
                else {
                    showErrorModal(updateResult.error.description);
                }
            } catch (e) {
                showErrorModal('An error occurred while loading the data.');
            }
        });

    });
    // Populates user form fields with the data
    function populateProjectForm(projectData) {
        $('#id').val(projectData.id);
        $('#name').val(projectData.name);
        $('#description').val(projectData.description);
    }
    function gatherFormData() {
        return {
            id: $('#id').val(),
            name: $('#name').val(),
            description: $('#description').val(),
        };
    }
});