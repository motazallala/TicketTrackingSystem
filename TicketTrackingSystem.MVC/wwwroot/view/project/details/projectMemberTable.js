import { initializeDataTable, reinitializeDataTable } from '../../../utility/dataTableUtility.js';
import { setupModalData } from '../../../utility/dataModalUtility.js';
import { setUserForProjectAsync, removeUserFromProjectAsync, getStageDropdown } from '../../../services/projectServices.js';

$(document).ready(function () {
    let isMember = false;
    const projectId = $('#projectId').val();

    // Initialize the DataTable on page load
    let userMemberTable = initializeDataTable({
        tableId: '#userMemberTable',
        apiUrl: 'https://localhost:7264/user/call',
        method: 'getprojectmembersasync',
        columns: getDataTableColumns(isMember),
        additionalParameters: [isMember, projectId],
        failureCallback: showErrorMessage
    });

    // Event delegation for handling click events dynamically
    $('#userMemberTable').on('click', '.dt-view', function () {
        const data = userMemberTable.row($(this).parents('tr')).data();
        showUserDetailsModal(data);
    }).on('click', '.dt-assign', function () {
        const data = userMemberTable.row($(this).parents('tr')).data();
        showAssignUserModal(data.id, data.userName, data.userType);
    }).on('click', '.dt-removeAssign', function () {
        const data = userMemberTable.row($(this).parents('tr')).data();
        showRemoveUserModal(data.id, data.userName);
    });

    // Toggle between showing users with and without roles
    $('#switchBtn').on('click', function () {
        this.innerHTML = isMember ? 'Show Users With This Project' : 'Show Users Not In the Project';
        isMember = !isMember;
        reinitializeTable(isMember, projectId);
    });

    // Reinitialize DataTable based on toggle
    function reinitializeTable(isMember, projectId) {
        userMemberTable = reinitializeDataTable({
            tableId: '#userMemberTable',
            apiUrl: 'https://localhost:7264/user/call',
            method: 'getprojectmembersasync',
            columns: getDataTableColumns(isMember),
            additionalParameters: [isMember, projectId],
            failureCallback: showErrorMessage
        });
    }

    // Function to get DataTable columns based on user role
    function getDataTableColumns(isMember) {
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
                    return isMember ? getActionButtonsWithRemove(data.id) : getActionButtonsWithAssign(data.id);
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
                <button class="btn btn-primary btn-sm me-2 dt-assign">
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
                <button class="btn btn-danger btn-sm me-2 dt-removeAssign">
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
        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [closeButton]);

        $('#myModal').modal('show');
    }




    async function showAssignUserModal(userId, userName,userType) {

        if (userType === 'Client') {
            // Show a modal with a message for clients
            const title = 'Information';
            const bodyContent = `<p>Assign ${userName} to project.</p>`;
            const confirmButton = `<button type="button" class="btn btn-primary" id="submitAssign">Yes</button>`;
            const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
            setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton,closeButton]);


            $('#myModal').modal('show');
            $('#submitAssign').off('click').on('click', async function () {
                await handleUserAssignment(userId, projectId,0);
            });
        } else if (userType === 'Member') {
            try {
                // Fetch dropdown options for stages from the server
                const stageDropdown = await getStageDropdown();

                const title = 'Assign Role';
                const bodyContent = `
                <p>Assign a stage to ${userName}:</p>
                <div class="form-group">
                    <label for="stageSelect">Stage</label>
                    <select class="form-control" id="stageSelect">
                        ${stageDropdown.data}
                    </select>
                </div>
            `;
                const confirmButton = `<button type="button" class="btn btn-primary" id="submitAssign">Yes</button>`;
                const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
                setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton, closeButton]);

                $('#myModal').modal('show');

                // Unbind previous events before binding a new one
                $('#submitAssign').off('click').on('click', async function () {
                    const selectedStage = $('#stageSelect').val(); // Get the selected stage value
                    await handleUserAssignment(userId, projectId, selectedStage);
                });
            } catch (error) {
                showErrorMessage({ code: '500', description: 'Error fetching stage dropdown.' });
            }
        } else {
            // Handle other cases if necessary
            showErrorMessage({ code: '400', description: 'Invalid user type.' });
        }
    }


    // Modal setup for assigning a user to a project
    //async function showAssignUserModal(userId, userName) {
    //    try {
    //        // Fetch dropdown options for stages from the server
    //        const stageDropdown = await getStageDropdown();

    //        const title = 'Assign Role';
    //        const bodyContent = `
    //            <p>Assign a stage to ${userName}:</p>
    //            <div class="form-group">
    //                <label for="stageSelect">Stage</label>
    //                <select class="form-control" id="stageSelect">
    //                    ${stageDropdown.data}
    //                </select>
    //            </div>
    //        `;
    //        const confirmButton = `<button type="button" class="btn btn-primary" id="submitAssign">Yes</button>`;
    //        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
    //        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton, closeButton]);

    //        $('#myModal').modal('show');

    //        // Unbind previous events before binding a new one
    //        $('#submitAssign').off('click').on('click', async function () {
    //            const selectedStage = $('#stageSelect').val(); // Get the selected stage value
    //            await handleUserAssignment(userId, projectId, selectedStage);
    //        });
    //    } catch (error) {
    //        showErrorMessage({ code: '500', description: 'Error fetching stage dropdown.' });
    //    }
    //}

    // Modal setup for removing a user from the project
    async function showRemoveUserModal(userId, userName) {
        const title = 'Remove User from Project';
        const bodyContent = `<p>Are you sure you want to remove ${userName} from this project?</p>`;
        const confirmButton = `<button type="button" class="btn btn-primary" id="submitRemove">Yes</button>`;
        const closeButton = `<button type="button" class="btn btn-default" data-dismiss="modal">No</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [confirmButton, closeButton]);

        $('#myModal').modal('show');

        // Unbind previous events before binding a new one
        $('#submitRemove').off('click').on('click', async function () {
            await handleUserRemoval(userId, projectId);
        });
    }

    // Handle user assignment to a project with a selected stage
    async function handleUserAssignment(userId, projectId, stage) {

        const result = await setUserForProjectAsync(userId, projectId, stage);
        if (result.isSuccess) {
            $('#myModal').modal('hide');
            userMemberTable.ajax.reload(null, false);
        } else {
            showErrorMessage(result.error.description);
        }

    }

    // Handle user removal from a project
    async function handleUserRemoval(userId, projectId) {

        const result = await removeUserFromProjectAsync(userId, projectId);
        if (result.isSuccess) {
            $('#myModal').modal('hide');
            userMemberTable.ajax.reload(null, false);
        } else {
            showErrorMessage(result.error.description);
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
