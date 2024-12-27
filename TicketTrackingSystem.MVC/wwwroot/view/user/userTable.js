import { initializeDataTable } from '../../utility/dataTableUtility.js';
import { setupModalData } from '../../utility/dataModalUtility.js';
let userTable;
$(document).ready(function () {
    userTable = initializeDataTable({
        tableId: '#userTable',
        apiUrl: 'https://localhost:7264/user/call',
        method: 'getalluserswithrolepaginatedasync',
        columns: [
            { data: 'id', name: 'ID' },
            { data: 'fullName', name: 'FullName' },
            { data: 'userName', name: 'UserName' },
            { data: 'email', name: 'Email' },
            { data: 'phoneNumber', name: 'PhoneNumber' },
            { data: 'userType', name: 'UserType' },
            { data: 'departmentName', name: 'DepartmentName', orderable: false },
            {
                data: 'roles', // Change this to 'roles' to access the list
                orderable: false,  // Disable sorting on this column
                name: 'Role Name',
                render: function (data, type, row) {
                    // Check if 'data' is an array (roles list) and join the role names with commas
                    return data && Array.isArray(data) ? data.map(role => `<a href="/Role/Details/${role.id}">${role.name}</a>`).join(' ,<br>') : '';
                }
            },
            {
                data: "id",
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    return `
                        <div class="d-flex justify-content-center">
                            <!-- Details Button -->
                            <button  class="btn btn-primary btn-sm me-2 dt-view">
                                <i class="bi bi-info-square-fill"></i> Details
                            </button>
                            <!-- Edit Button -->
                            <button class="btn btn-warning btn-sm me-2 dt-edit">
                                <i class="bi bi-pencil-square"></i> Edit
                            </button>
                            <!-- Delete Button -->
                            <button class="btn btn-danger btn-sm dt-delete">
                                <i class="bi bi-trash-fill"></i> Delete
                            </button>
                        </div>
                    `;
                }
            }

        ],
        additionalParameters: null,
        failureCallback: showErrorMessage
        
    });
    userTable.on('click', '.dt-view', function () {
        let data = userTable.row($(this).parents('tr')).data();

        // Modal elements
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'User Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">User Name:</label>
                <input type="text" class="form-control" id="userName" name="userName" value="${data.userName}" disabled>
            </div>
            <div class="form-group">
                <label for="description">Email:</label>
                <input type="text" class="form-control" id="email" name="email" value="${data.email}" disabled>
            </div>
            <div class="form-group">
                <label for="description">Phone Number:</label>
                <input type="text" class="form-control" id="phoneNumber" name="phoneNumber" value="${data.phoneNumber}" disabled>
            </div>
`;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="${data.id}">View</a>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, moreDetails]);
        $('#myModal').modal('show');
    });

    //function to make the model show error message from data table call back function
    function showErrorMessage(error) {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = `Error ${error.code}`;
        const bodyContent = `<p>${error.description}</p>`;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton]);
        $('#myModal').modal('show');
    }



});
export { userTable };