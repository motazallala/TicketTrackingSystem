import { roleTable } from './roleForPermissionTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { addRoleToPermissionAsync, getAllPermissionsAsHtmlAsync } from '../../services/permissionServices.js';
import { getAllRolesAsHtmlAsync } from '../../services/roleServices.js';
$(document).ready(function () {
    // Cache for dropdown options to avoid repeated server calls
    let cachedRoles = null;
    let cachedPermissions = null;
    $("#addBtn").on("click", async function () {
        //add Permission modal that has a name and description field and a submit button
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Add Permission';
        const bodyContent = `
            <div class="form-group">
                <label for="roleId">Role :</label>
                <select class="form-control" id="roleId" name="roleId"></select>
            </div>
            <div class="form-group" id="permissionDropDown">
                <label for="permissionIds">Permission :</label>
                <select class="form-select" size="19" aria-label="size 3 select example" id="permissionIds" name="permissionIds" multiple>
                </select>
            </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);

        try {
            await populateDropdowns();
            $('#myModal').modal('show');

            $('#submit').off('click').on('click', async function () {
                const createPermissionDto = gatherFormData();

                const addResult = await addRoleToPermissionAsync(JSON.stringify(createPermissionDto));
                if (addResult.isSuccess) {
                    $('.modal').modal('hide');
                    roleTable.ajax.reload(null, false);
                }
                else {
                    showErrorModal(addResult.error.description);
                }

            });
        }
        catch (e) {
            showErrorModal('An error occurred while loading the data.');
        }
    });

    function gatherFormData() {
        return {
            roleId: $('#roleId').val(),
            permissionIds: $('#permissionIds').val()
        };
    }

    async function populateDropdowns() {
        if (!cachedRoles) {
            cachedRoles = await getAllRolesAsHtmlAsync();
        }
        if (cachedRoles.isSuccess) {
            $('#roleId').html(cachedRoles.data);
        }
        else {
            showErrorModal(cachedRoles.error.description);
        }
        if (!cachedPermissions) {
            cachedPermissions = await getAllPermissionsAsHtmlAsync();
        }
        if (cachedPermissions.isSuccess) {
            $('#permissionIds').html(cachedPermissions.data);
        }
        else {
            showErrorModal(cachedPermissions.error.description);
        }
    }
});