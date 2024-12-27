﻿import { initializeDataTable } from '../../utility/dataTableUtility.js';
import { setupModalData } from '../../utility/dataModalUtility.js';

let roleTable;

$(document).ready(function () {
    roleTable = initializeDataTable({
        tableId: '#roleTable',
        apiUrl: 'https://localhost:7264/role/call',
        method: 'getallrolespaginatedasync',
        columns: [
            { data: 'id', name: 'ID' },
            { data: 'name', name: 'Name' },
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
                            <button class="btn btn-warning btn-sm me-2 dt-edit">
                                <i class="bi bi-pencil-square"></i> Edit
                            </button>
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

    // Row click event handler
    roleTable.on('click', '.dt-view', function () {
        let data = roleTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Role Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id-view" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name-view" name="name" value="${data.name}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Role/Details/${data.id}">View</a>`;
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
export { roleTable };
