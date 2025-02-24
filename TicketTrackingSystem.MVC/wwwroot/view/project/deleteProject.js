import { projectTable } from './projectTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { deleteProjectAsync, deleteProjectCascadeAsync } from '../../services/projectServices.js';
import { showGlobalSpinner, hideGlobalSpinner } from '../../utility/spinnerUtility.js';

$(document).ready(function () {
    projectTable.on("click", '.dt-delete', async function () {
        const $this = $(this);
        const dtRow = $this.parents('tr');
        const data = projectTable.row(dtRow).data();

        //model elemants
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content
        let title = 'Delete Project';
        let bodyContent = `<p>Are you sure you want to delete Project : ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteBtn">Delete</button>`;
        const deleteButtonCascade = `<button class="btn btn-danger"  id="deleteProjectWithCascade">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteBtn').off('click').on('click', async function () {
            const projectId = data.id;
            showGlobalSpinner();
            const deleteResult = await deleteProjectAsync(projectId);
            hideGlobalSpinner();
            if (deleteResult.isSuccess) {
                projectTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else {
                if (deleteResult.error.description === 'There is member or opened tickets do you what to delete all related member and ticket?') {
                    bodyContent = `<p>Are you sure you want to delete project : ${data.name}?</p>
                        <p>Deleting this project will also delete all associated project member and ticket.</p>`;
                    title = 'Delete Project With Cascade';
                    setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButtonCascade, cancelButton]);
                    $('#deleteProjectWithCascade').off('click').on('click', async function () {
                        const deleteResult = await deleteProjectCascadeAsync(projectId);

                        if (deleteResult.isSuccess) {
                            projectTable.ajax.reload();
                            $('#myModal').modal('hide');
                        }
                        else {
                            showErrorModal(deleteResult.error.description);
                        }

                    });
                }
                else {
                    showErrorModal(deleteResult.error.description);
                }
            }
        });
    });
});