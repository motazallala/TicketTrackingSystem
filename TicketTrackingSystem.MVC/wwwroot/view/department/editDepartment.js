import { departmentTable } from './departmentTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { updateDepartmentAsync } from '../../services/departmentServices.js';

$(document).ready(function () {
    departmentTable.on('click', '.dt-edit', async function () {
        let data = departmentTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Edit Department';
        const bodyContent = `
                <input type="hidden" class="form-control" id="id" name="id" value="${data.id}" >
            <div class="form-group">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name" name="name" value="${data.name}" >
            </div>
            <div class="form-group">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="description" name="description" value="${data.description}">
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const editDepartment = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, editDepartment]);
        $('#myModal').modal('show');
        $('#submit').on('click', async function () {
            const editDepartmentDto = {
                id: $('#id').val().trim(),
                name: $('#name').val().trim(),
                description: $('#description').val().trim(),
            };
            const updateResult = await updateDepartmentAsync(JSON.stringify(editDepartmentDto))
            if (updateResult.isSuccess) {

                $('.modal').modal('hide');
                departmentTable.ajax.reload(null, false);
            }
            else {
                showErrorModal(updateResult.error.description);
            }
        });

    });
});