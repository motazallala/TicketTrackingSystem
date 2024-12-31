import { initializeDataTable } from '../../../utility/dataTableUtility.js';
import { setupModalData } from '../../../utility/dataModalUtility.js';

let userProjectTable;

$(document).ready(function () {
    userProjectTable = initializeDataTable({
        tableId: '#userProjectTable',
        apiUrl: 'https://localhost:7264/projectforclient/call',
        method: 'getalluserprojectsasync',
        columns: [
            { data: 'id', name: 'ID' },
            { data: 'name', name: 'Name' },
            { data: 'description', name: 'Description' },
            { 
                data: 'createdAt', 
                name: 'CreatedAt',
                render: function (data, type, row) {
                    return new Date(data).toLocaleString();
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
                            <button class="btn btn-primary btn-sm me-2 dt-view">
                                <i class="bi bi-info-square-fill"></i> Details
                            </button>
                        </div>
                    `;
                }
            }
        ],
        additionalParameters: null,
        failureCallback: showErrorMessage
    });

    // Row click event handler
    userProjectTable.on('click', '.dt-view', function () {
        let data = userProjectTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Project Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id-view" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name-view" name="name" value="${data.name}" disabled>
            </div>
            <div class="form-group">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="name-view" name="description" value="${data.description}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Project/Tickets/${data.id}">View Project Tickets</a>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, moreDetails]);
        $('#myModal').modal('show');
    });

    // Function to handle errors
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
    $('#myModal').on('hidden.bs.modal', function (evt) {
        $('.modal .modal-title').empty();
        $('.modal .modal-body').empty();
        $('.modal .modal-footer').empty();
    });
});

// Export the DataTable instance when it's ready
export { userProjectTable };
