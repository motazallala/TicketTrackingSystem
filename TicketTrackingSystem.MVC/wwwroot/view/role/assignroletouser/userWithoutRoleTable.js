import { initializeDataTable, reinitializeDataTable } from '../../../utility/dataTableUtility.js';
import { setupModalData } from '../../../utility/dataModalUtility.js';
import { setRoleToUserAsync, removeRoleFromUserAsync } from '../../../services/userServices.js';

$(document).ready(function () {
    let userWithRole = false;
    const roleId = $('#roleId').val();

    // Initialize the DataTable on page load
    let userWithoutRoleTable = initializeDataTable({
        tableId: '#userWithoutRoleTable',
        apiUrl: 'https://localhost:7264/user/call',
        method: 'getalluserswithrolewithconditionpaginatedasync',
        columns: getDataTableColumns(userWithRole),
        additionalParameters: [userWithRole, roleId],
        failureCallback: showErrorMessage
    });

    // Event delegation for handling click events dynamically
    $('#userWithoutRoleTable').on('click', '.dt-view', function () {
        const data = userWithoutRoleTable.row($(this).parents('tr')).data();
        showUserDetailsModal(data);
    }).on('click', '.dt-assing', function () {
        const data = userWithoutRoleTable.row($(this).parents('tr')).data();
        showAssignRoleModal(data.id, data.userName);
    }).on('click', '.dt-removeAssing', function () {
        const data = userWithoutRoleTable.row($(this).parents('tr')).data();
        showRemoveRoleModal(data.id, data.userName);
    });

    // Toggle between showing users with and without roles
    $('#switchBtn').on('click', function () {
        this.innerHTML = userWithRole ? 'Show Users With Role' : 'Show Users Without Role';
        userWithRole = !userWithRole;

        reinitializeTable(userWithRole, roleId);
    });

    // Reinitialize DataTable based on toggle
    function reinitializeTable(userWithRolepar, roleIdpar) {
        userWithoutRoleTable = reinitializeDataTable({
            tableId: '#userWithoutRoleTable',
            apiUrl: 'https://localhost:7264/user/call',
            method: 'getalluserswithrolewithconditionpaginatedasync',
            columns: getDataTableColumns(userWithRolepar),
            additionalParameters: [userWithRolepar, roleIdpar],
            failureCallback: showErrorMessage
        });
    }

    // Common function to get DataTable columns with userWithRole toggle
    function getDataTableColumns(userWithRole) {
        return [
            { data: 'id', name: 'ID' },
            { data: 'fullName', name: 'FullName' },
            { data: 'userName', name: 'UserName' },
            { data: 'email', name: 'Email' },
            { data: 'phoneNumber', name: 'PhoneNumber' },
            { data: 'userType', name: 'UserType' },
            { data: 'departmentName', name: 'DepartmentName', orderable: false },
            {
                data: 'roles',
                name: 'Role Name',
                orderable: false,
                render: function (data) {
                    return data && Array.isArray(data) ? data.map(role => `<a href="/Role/Details/${role.id}">${role.name}</a>`).join(' ,<br>') : '';
                }
            },
            {
                data: 'id',
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    if (userWithRole) {
                        return getActionButtonsWithRemove(data.id);
                    } else {
                        return getActionButtonsWithAssign(data.id);
                    }
                }
            }
        ];
    }

    // Action buttons for assigning a role
    function getActionButtonsWithAssign(userId) {
        return `
            <div class="d-flex justify-content-center">
                <button class="btn btn-secondary btn-sm me-2 dt-view">
                    <i class="bi bi-info-square-fill"></i> Details
                </button>
                <button class="btn btn-primary btn-sm me-2 dt-assing">
                    Assign <i class="bi bi-plus-square-fill"></i>
                </button>
            </div>
        `;
    }

    // Action buttons for removing a role
    function getActionButtonsWithRemove(userId) {
        return `
            <div class="d-flex justify-content-center">
                <button class="btn btn-secondary btn-sm me-2 dt-view">
                    <i class="bi bi-info-square-fill"></i> Details
                </button>
                <button class="btn btn-danger btn-sm me-2 dt-removeAssing">
                    Remove <i class="bi bi-trash3-fill"></i>
                </button>
            </div>
        `;
    }

    // Modal setup for showing user details
    function showUserDetailsModal(data) {
        const title = 'User Details';
        const bodyContent = `
            <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">User Name:</label>
                <input type="text" class="form-control" value="${data.userName}" disabled>
            </div>
            <div class="form-group">
                <label for="email">Email:</label>
                <input type="text" class="form-control" value="${data.email}" disabled>
            </div>
            <div class="form-group">
                <label for="phoneNumber">Phone Number:</label>
                <input type="text" class="form-control" value="${data.phoneNumber}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [viewButton]);

        $('#myModal').modal('show');
    }

    // Modal setup for assigning a role
    function showAssignRoleModal(userId, userName) {
        const title = 'Assign Role';
        const bodyContent = `<p>Are you sure you want to assign this role to ${userName}?</p>`;
        const confirmButton = `<button type="button" class="btn btn-primary" id="submitAssign">Yes</button>`;
        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton, closeButton]);

        $('#myModal').modal('show');

        // Unbind previous events before binding new one
        $('#submitAssign').off('click').on('click', async function () {
            await handleRoleAssignment(userId, roleId);
        });
    }

    // Modal setup for removing a role
    function showRemoveRoleModal(userId, userName) {
        const title = 'Remove Assign Role';
        const bodyContent = `<p>Are you sure you want to remove this role from ${userName}?</p>`;
        const confirmButton = `<button type="button" class="btn btn-primary" id="submitRemove">Yes</button>`;
        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton, closeButton]);

        $('#myModal').modal('show');

        // Unbind previous events before binding new one
        $('#submitRemove').off('click').on('click', async function () {
            await handleRoleRemoval(userId, roleId);
        });
    }

    // Handle role assignment
    async function handleRoleAssignment(userId, roleId) {
        try {
            const result = await setRoleToUserAsync(userId, roleId);
            if (result.isSuccess) {
                $('#myModal').modal('hide');
                userWithoutRoleTable.ajax.reload(null, false);
            } else {
                showErrorMessage(result.error);
            }
        } catch (error) {
            showErrorMessage({ code: '500', description: 'An error occurred while assigning the role.' });
        }
    }

    // Handle role removal
    async function handleRoleRemoval(userId, roleId) {
        try {
            const result = await removeRoleFromUserAsync(userId, roleId);
            if (result.isSuccess) {
                $('#myModal').modal('hide');
                userWithoutRoleTable.ajax.reload(null, false);
            } else {
                showErrorMessage(result.error);
            }
        } catch (error) {
            showErrorMessage({ code: '500', description: 'An error occurred while removing the role.' });
        }
    }

    // Show error message modal
    function showErrorMessage(error) {
        const title = `Error ${error.code}`;
        const bodyContent = `<p>${error.description}</p>`;
        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [closeButton]);

        $('#myModal').modal('show');
    }
});
